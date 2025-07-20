//
// FallDetect - FallDetectionService.cs
// Developed by Sarath Reddy Konda, 2025
//
// Copyright (c) 2025 Sarath Konda
// Licensed under the MIT License. See LICENSE file in the project root.
//
// This service detects falls based on device motion and acceleration patterns.
//

using CoreMotion;
using Foundation;
using System;
using System.Threading.Tasks;

namespace FallDetect
{
    public class FallDetectionService
    {
        private readonly CMMotionManager motionManager;
        private const double AccelerationThreshold = 2.5; // G-forces threshold
        private const double InactivityDuration = 5.0; // seconds
        private DateTime? fallDetectedAt;

        public event Action OnFallDetected;

        public FallDetectionService()
        {
            motionManager = new CMMotionManager
            {
                AccelerometerUpdateInterval = 0.2
            };
        }

        public void StartMonitoring()
        {
            if (!motionManager.AccelerometerAvailable)
                return;

            motionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, (data, error) =>
            {
                if (data == null || error != null)
                    return;

                double totalAccel = Math.Sqrt(
                    data.Acceleration.X * data.Acceleration.X +
                    data.Acceleration.Y * data.Acceleration.Y +
                    data.Acceleration.Z * data.Acceleration.Z);

                if (totalAccel > AccelerationThreshold)
                {
                    fallDetectedAt = DateTime.UtcNow;
                }
                else if (fallDetectedAt.HasValue &&
                         (DateTime.UtcNow - fallDetectedAt.Value).TotalSeconds > InactivityDuration)
                {
                    fallDetectedAt = null;
                    OnFallDetected?.Invoke();
                }
            });
        }

        public void StopMonitoring()
        {
            if (motionManager.AccelerometerActive)
            {
                motionManager.StopAccelerometerUpdates();
            }
        }
    }
}
