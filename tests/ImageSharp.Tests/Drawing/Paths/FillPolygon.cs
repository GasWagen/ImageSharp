// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System.Numerics;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Primitives;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Drawing;
using SixLabors.ImageSharp.Tests.Processing;
using SixLabors.ImageSharp.Tests.TestUtilities;
using SixLabors.Shapes;
using Xunit;

namespace SixLabors.ImageSharp.Tests.Drawing.Paths
{
    public class FillPolygon : BaseImageOperationsExtensionTest
    {
        private static readonly GraphicsOptionsComparer graphicsOptionsComparer = new GraphicsOptionsComparer();

        GraphicsOptions nonDefault = new GraphicsOptions { Antialias = false };
        Color color = Color.HotPink;
        SolidBrush brush = Brushes.Solid(Rgba32.HotPink);
        SixLabors.Primitives.PointF[] path = {
                    new Vector2(10,10),
                    new Vector2(20,10),
                    new Vector2(20,10),
                    new Vector2(30,10),
                };


        [Fact]
        public void CorrectlySetsBrushAndPath()
        {
            this.operations.FillPolygon(this.brush, this.path);

            FillRegionProcessor processor = this.Verify<FillRegionProcessor>();

            Assert.Equal(new GraphicsOptions(), processor.Options, graphicsOptionsComparer);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            Polygon polygon = Assert.IsType<Polygon>(region.Shape);
            Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

            Assert.Equal(this.brush, processor.Brush);
        }

        [Fact]
        public void CorrectlySetsBrushPathAndOptions()
        {
            this.operations.FillPolygon(this.nonDefault, this.brush, this.path);
            FillRegionProcessor processor = this.Verify<FillRegionProcessor>();

            Assert.Equal(this.nonDefault, processor.Options, graphicsOptionsComparer);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            Polygon polygon = Assert.IsType<Polygon>(region.Shape);
            Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

            Assert.Equal(this.brush, processor.Brush);
        }

        [Fact]
        public void CorrectlySetsColorAndPath()
        {
            this.operations.FillPolygon(this.color, this.path);
            FillRegionProcessor processor = this.Verify<FillRegionProcessor>();


            Assert.Equal(new GraphicsOptions(), processor.Options, graphicsOptionsComparer);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            Polygon polygon = Assert.IsType<Polygon>(region.Shape);
            Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(this.color, brush.Color);
        }

        [Fact]
        public void CorrectlySetsColorPathAndOptions()
        {
            this.operations.FillPolygon(this.nonDefault, this.color, this.path);
            FillRegionProcessor processor = this.Verify<FillRegionProcessor>();

            Assert.Equal(this.nonDefault, processor.Options, graphicsOptionsComparer);

            ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
            Polygon polygon = Assert.IsType<Polygon>(region.Shape);
            Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

            SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
            Assert.Equal(this.color, brush.Color);
        }
    }
}
