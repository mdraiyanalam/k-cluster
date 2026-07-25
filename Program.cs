using System;
using System.ComponentModel.Design;
using System.Linq;

public static class KMeans3D
{
    public static double[][] Cluster(double[][] points, int k, int maxIterations = 100, int? seed = null)
    {
        if (points == null || points.Length == 0)
            throw new ArgumentException("Points array is empty");
        if (k <= 0 || k > points.Length)
            throw new ArgumentException("Invalid k");

        int n = points.Length;
        var rnd = seed.HasValue ? new Random(seed.Value) : new Random();

        // 1. Initializing centroids by randomly selecting k distinct points
        var centroids = new double[k][];
        var used = new bool[n];
        for (int i = 0; i < k; i++)
        {
            int idx;
            do { idx = rnd.Next(n); } while (used[idx]);
            used[idx] = true;
            centroids[i] = (double[])points[idx].Clone();
        }

        var assignments = new int[n];

        for (int iter = 0; iter < maxIterations; iter++)
        {
            // 2. Assiging Clusters Coordinates
            bool changed = false;
            for (int i = 0; i < n; i++)
            {
                int best = 0;
                double bestDist = DistanceSquared(points[i], centroids[0]);
                for (int c = 1; c < k; c++)
                {
                    double d = DistanceSquared(points[i], centroids[c]);
                    if (d < bestDist)
                    {
                        bestDist = d;
                        best = c;
                    }
                }
                if (assignments[i] != best)
                {
                    assignments[i] = best;
                    changed = true;
                }
            }

            if (!changed) break; // converged

            // 3. Updating centroids with mean of assigned points
            var sums = new double[k][];
            var counts = new int[k];
            for (int c = 0; c < k; c++)
                sums[c] = new double[3];

            for (int i = 0; i < n; i++)
            {
                int c = assignments[i];
                sums[c][0] += points[i][0];
                sums[c][1] += points[i][1];
                sums[c][2] += points[i][2];
                counts[c]++;
            }

            for (int c = 0; c < k; c++)
            {
                if (counts[c] > 0)
                {
                    centroids[c][0] = sums[c][0] / counts[c];
                    centroids[c][1] = sums[c][1] / counts[c];
                    centroids[c][2] = sums[c][2] / counts[c];
                }
                else { }
            }
        }

        return centroids;
    }

    private static double DistanceSquared(double[] a, double[] b)
    {
        double dx = a[0] - b[0];
        double dy = a[1] - b[1];
        double dz = a[2] - b[2];
        return dx * dx + dy * dy + dz * dz;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Hardcoded 3D points for clustering for Easy Demonstration instead of user input
            double[][] points = new double[][]
            {
                new double[] { 0.1,  0.2,  0.0 },
                new double[] { 0.0,  0.1,  0.1 },
                new double[] {-0.1,  0.0,  0.0 },
                new double[] { 5.1,  4.9,  5.0 },
                new double[] { 4.8,  5.2,  5.1 },
                new double[] { 5.0,  5.0,  4.9 },
                new double[] { 9.9,  0.1, 10.1 },
                new double[] {10.1, -0.1,  9.9 },
                new double[] {10.0,  0.0, 10.0 }
            };

            Console.WriteLine("K-Means Clustering for 3D Points\n");

            // Create and run
            double[][] centroids = KMeans3D.Cluster(points, k: 3, maxIterations: 50, seed: 42);

            // Display result
            Console.WriteLine("Final cluster centroids (coordinates):");
            for (int i = 0; i < centroids.Length; i++)
            {
                Console.WriteLine($"  Cluster {i}: " +
                    $"({centroids[i][0]:F4}, {centroids[i][1]:F4}, {centroids[i][2]:F4})");
            }

            Console.WriteLine("\nProgeam Completed.");
            Console.ReadKey();
        }
    }
}