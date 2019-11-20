#!/bin/bash

# Build in release mode
dotnet build -c Release -f netcoreapp3.0

# Run benchmarks
dotnet bin/Release/netcoreapp3.0/ImageSharp.Benchmarks.dll
