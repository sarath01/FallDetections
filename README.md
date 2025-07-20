# FallDetect.iOS

**FallDetect** is a .NET iOS library that uses CoreMotion to detect sudden falls based on accelerometer data. Ideal for elder care and fall-response apps.

## Features

- Detects sudden falls using device motion
- Threshold-based acceleration check
- Optional inactivity window to reduce false positives
- Built with .NET 8 and Xamarin.iOS

## Usage

```csharp
var detector = new FallDetectionService();
detector.OnFallDetected += () => Console.WriteLine("Fall detected!");
detector.StartMonitoring();
```

## License

MIT License © 2025 Sarath Konda  
Built in support of the EB2-NIW petition to advance public-interest health tech.
