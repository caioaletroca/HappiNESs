using System;
using System.Collections.Generic;

namespace HappiNESs.Helpers
{
    /// <summary>
    /// Helpers for math operations
    /// </summary>
    public class MathHelpers
    {
        /// <summary>
        /// Calculates the interpolation of a curve to maintain the same curve with less points
        /// </summary>
        /// <param name="data">The curve to be interpolated</param>
        /// <param name="threshold">The number of points</param>
        /// <returns></returns>
        public static IEnumerable<Tuple<double, double>> LargestTriangleThreeBuckets(List<Tuple<double, double>> data, int threshold)
        {
            var dataLength = data.Count;
            if (threshold >= dataLength || threshold == 0)
                return data; // Nothing to do

            var sampled = new List<Tuple<double, double>>(threshold);

            // Bucket size. Leave room for start and end data points
            var every = (double)(dataLength - 2) / (threshold - 2);

            var a = 0;
            var maxAreaPoint = new Tuple<double, double>(0, 0);
            var nextA = 0;

            sampled.Add(data[a]); // Always add the first point

            for (var i = 0; i < threshold - 2; i++)
            {
                // Calculate point average for next bucket (containing c)
                double avgX = 0;
                double avgY = 0;
                var avgRangeStart = (int)(Math.Floor((i + 1) * every) + 1);
                var avgRangeEnd = (int)(Math.Floor((i + 2) * every) + 1);
                avgRangeEnd = avgRangeEnd < dataLength ? avgRangeEnd : dataLength;

                var avgRangeLength = avgRangeEnd - avgRangeStart;

                for (; avgRangeStart < avgRangeEnd; avgRangeStart++)
                {
                    avgX += data[avgRangeStart].Item1; // * 1 enforces Number (value may be Date)
                    avgY += data[avgRangeStart].Item2;
                }
                avgX /= avgRangeLength;

                avgY /= avgRangeLength;

                // Get the range for this bucket
                var rangeOffs = (int)(Math.Floor((i + 0) * every) + 1);
                var rangeTo = (int)(Math.Floor((i + 1) * every) + 1);

                // Point a
                var pointAx = data[a].Item1; // enforce Number (value may be Date)
                var pointAy = data[a].Item2;

                double maxArea = -1;

                for (; rangeOffs < rangeTo; rangeOffs++)
                {
                    // Calculate triangle area over three buckets
                    var area = Math.Abs((pointAx - avgX) * (data[rangeOffs].Item2 - pointAy) -
                                           (pointAx - data[rangeOffs].Item1) * (avgY - pointAy)
                                      ) * 0.5;
                    if (area > maxArea)
                    {
                        maxArea = area;
                        maxAreaPoint = data[rangeOffs];
                        nextA = rangeOffs; // Next a is this b
                    }
                }

                sampled.Add(maxAreaPoint); // Pick this point from the bucket
                a = nextA; // This a is the next a (chosen b)
            }

            sampled.Add(data[dataLength - 1]); // Always add last

            return sampled;
        }
    }
}
