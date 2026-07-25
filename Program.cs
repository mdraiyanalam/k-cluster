using System;
using System.Collections.Generic;

namespace ThreeDImensionArray
{
    public class KMeansFromMedium
    {
        // Squared Euclidean distance
        private static double SquaredDistance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return sum;
        }

        // Select k random points as initial centroids
        private static List<double[]> GetRandomCentroids(List<double[]> dataset, int k, Random rnd)
        {
            var centroids = new List<double[]>();
            var used = new HashSet<int>();

            while (centroids.Count < k)
            {
                int index = rnd.Next(dataset.Count);
                if (!used.Contains(index))
                {
                    used.Add(index);
                    centroids.Add((double[])dataset[index].Clone());
                }
            }
            return centroids;
        }

        // Assign every point to the nearest centroid
        private static List<List<double[]>> AssignPoints(List<double[]> dataset, List<double[]> centroids)
        {
            var clusters = new List<List<double[]>>();
            for (int i = 0; i < centroids.Count; i++)
                clusters.Add(new List<double[]>());

            foreach (var point in dataset)
            {
                int closest = 0;
                double minDist = SquaredDistance(point, centroids[0]);

                for (int i = 1; i < centroids.Count; i++)
                {
                    double dist = SquaredDistance(point, centroids[i]);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closest = i;
                    }
                }
                clusters[closest].Add(point);
            }
            return clusters;
        }

        // Recalculate centroids
        private static List<double[]> CalculateNewCentroids(List<List<double[]>> clusters, int dimensions)
        {
            var newCentroids = new List<double[]>();

            foreach (var cluster in clusters)
            {
                if (cluster.Count == 0)
                {
                    newCentroids.Add(new double[dimensions]);
                    continue;
                }

                var mean = new double[dimensions];
                foreach (var point in cluster)
                {
                    for (int d = 0; d < dimensions; d++)
                        mean[d] += point[d];
                }

                for (int d = 0; d < dimensions; d++)
                    mean[d] /= cluster.Count;

                newCentroids.Add(mean);
            }
            return newCentroids;
        }

        // Main K-Means function
        public static (List<double[]> centroids, List<List<double[]>> clusters)
            KMeans(List<double[]> dataset, int k, int maxIterations = 100)
        {
            if (dataset == null || dataset.Count == 0)
                throw new ArgumentException("Dataset is empty");

            int dimensions = dataset[0].Length;
            var rnd = new Random();

            var centroids = GetRandomCentroids(dataset, k, rnd);
            List<List<double[]>> clusters = new List<List<double[]>>();   // ← fixed nullability

            for (int iter = 0; iter < maxIterations; iter++)
            {
                clusters = AssignPoints(dataset, centroids);
                var newCentroids = CalculateNewCentroids(clusters, dimensions);

                bool converged = true;
                for (int i = 0; i < k; i++)
                {
                    if (SquaredDistance(centroids[i], newCentroids[i]) > 1e-9)
                    {
                        converged = false;
                        break;
                    }
                }

                centroids = newCentroids;
                if (converged) break;
            }

            return (centroids, clusters);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var points = new List<double[]>
            {
                new double[] { 0.1, 0.2, 0.0 },
                new double[] { 0.0, 0.1, 0.1 },
                new double[] {-0.1, 0.0, 0.0 },
                new double[] { 5.1, 4.9, 5.0 },
                new double[] { 4.8, 5.2, 5.1 },
                new double[] { 5.0, 5.0, 4.9 },
                new double[] { 9.9, 0.1,10.1 },
                new double[] {10.1,-0.1, 9.9 },
                new double[] {10.0, 0.0,10.0 }
            };

            Console.WriteLine("K-Means\n");

            var (centroids, clusters) = KMeansFromMedium.KMeans(points, k: 3);

            Console.WriteLine("Final Centroids:");
            for (int i = 0; i < centroids.Count; i++)
            {
                var c = centroids[i];
                Console.WriteLine($"  Cluster {i}: ({c[0]:F3}, {c[1]:F3}, {c[2]:F3})");
            }

            Console.WriteLine("\nDone.");
            Console.ReadKey();
        }
    }
}