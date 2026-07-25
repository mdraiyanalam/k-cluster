using System;

namespace KMeans3D
{
    public class KMeans
    {
        public double[][] data;      // source points
        public int k;                // number of clusters
        public int N;                // number of points
        public int dim;              // dimension (will be 3)
        public int trials;           // how many times to restart
        public int maxIter;          // max iterations per run
        public Random rnd;
        public int[] clustering;     // cluster id of each point
        public double[][] means;     // the centroids we return

        public KMeans(double[][] data, int k)
        {
            this.data = data;
            this.k = k;
            this.N = data.Length;
            this.dim = data[0].Length;          // should be 3
            this.trials = Math.Max(10, N);      // sensible default
            this.maxIter = N * 2;
            Initialize(0);
        }

        // Random-partition initialization (exactly as in the article)
        public void Initialize(int seed)
        {
            rnd = new Random(seed);
            clustering = new int[N];
            means = new double[k][];
            for (int i = 0; i < k; ++i)
                means[i] = new double[dim];

            int[] indices = new int[N];
            for (int i = 0; i < N; ++i) indices[i] = i;
            Shuffle(indices);

            // first k points get unique cluster IDs
            for (int i = 0; i < k; ++i)
                clustering[indices[i]] = i;

            // remaining points get random cluster IDs
            for (int i = k; i < N; ++i)
                clustering[indices[i]] = rnd.Next(0, k);

            UpdateMeans();
        }

        private void Shuffle(int[] indices)
        {
            for (int i = 0; i < indices.Length; ++i)
            {
                int r = rnd.Next(i, indices.Length);
                int tmp = indices[i];
                indices[i] = indices[r];
                indices[r] = tmp;
            }
        }

        private static double SumSquared(double[] a, double[] b)
        {
            double sum = 0.0;
            for (int i = 0; i < a.Length; ++i)
            {
                double d = a[i] - b[i];
                sum += d * d;
            }
            return sum;
        }

        private static int ArgMin(double[] v)
        {
            int minIdx = 0;
            double minVal = v[0];
            for (int i = 1; i < v.Length; ++i)
            {
                if (v[i] < minVal)
                {
                    minVal = v[i];
                    minIdx = i;
                }
            }
            return minIdx;
        }

        private static bool AreEqual(int[] a, int[] b)
        {
            for (int i = 0; i < a.Length; ++i)
                if (a[i] != b[i]) return false;
            return true;
        }

        private static int[] Copy(int[] src)
        {
            int[] dst = new int[src.Length];
            Array.Copy(src, dst, src.Length);
            return dst;
        }

        // Recompute centroids from current clustering
        public bool UpdateMeans()
        {
            int[] counts = new int[k];
            for (int i = 0; i < N; ++i)
                counts[clustering[i]]++;

            for (int c = 0; c < k; ++c)
                if (counts[c] == 0) return false;   // empty cluster

            // reset
            for (int c = 0; c < k; ++c)
            {
                counts[c] = 0;
                for (int j = 0; j < dim; ++j) means[c][j] = 0.0;
            }

            for (int i = 0; i < N; ++i)
            {
                int c = clustering[i];
                counts[c]++;
                for (int j = 0; j < dim; ++j)
                    means[c][j] += data[i][j];
            }

            for (int c = 0; c < k; ++c)
                for (int j = 0; j < dim; ++j)
                    means[c][j] /= counts[c];

            return true;
        }

        // Assign every point to the nearest centroid
        public bool UpdateClustering()
        {
            int[] newClustering = new int[N];
            double[] distances = new double[k];

            for (int i = 0; i < N; ++i)
            {
                for (int c = 0; c < k; ++c)
                    distances[c] = SumSquared(data[i], means[c]);

                newClustering[i] = ArgMin(distances);
            }

            if (AreEqual(clustering, newClustering))
                return false;   // no change → converged

            // check for empty clusters
            int[] counts = new int[k];
            for (int i = 0; i < N; ++i)
                counts[newClustering[i]]++;

            for (int c = 0; c < k; ++c)
                if (counts[c] == 0) return false;

            // accept the new assignment
            for (int i = 0; i < N; ++i)
                clustering[i] = newClustering[i];

            return true;
        }

        // One full run of Lloyd’s algorithm
        public int[] ClusterOnce()
        {
            int iter = 0;
            while (iter < maxIter)
            {
                if (!UpdateClustering()) break;
                if (!UpdateMeans()) break;
                iter++;
            }
            return clustering;
        }

        public double WCSS()
        {
            double sum = 0.0;
            for (int i = 0; i < N; ++i)
                sum += SumSquared(data[i], means[clustering[i]]);
            return sum;
        }

        // Run many trials and keep the best result (lowest WCSS)
        public double[][] Cluster()
        {
            double bestWCSS = WCSS();
            int[] bestClustering = Copy(clustering);
            double[][] bestMeans = new double[k][];
            for (int c = 0; c < k; ++c)
                bestMeans[c] = (double[])means[c].Clone();

            for (int t = 1; t < trials; ++t)
            {
                Initialize(t);               // new random start
                ClusterOnce();
                double wcss = WCSS();

                if (wcss < bestWCSS)
                {
                    bestWCSS = wcss;
                    bestClustering = Copy(clustering);
                    for (int c = 0; c < k; ++c)
                        bestMeans[c] = (double[])means[c].Clone();
                }
            }

            // restore best result
            clustering = bestClustering;
            means = bestMeans;
            return means;                   // ← the required output
        }
    }

    // -------------------------------------------------------
    // Example usage for 3-D points
    // -------------------------------------------------------
    class Program
    {
        static void Main()
        {
            // Example 3-D points
            double[][] points = new double[][]
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

            Console.WriteLine("Running k-means for 3-D points (k = 3)...\n");

            KMeans km = new KMeans(points, k: 3);
            double[][] centroids = km.Cluster();   // returns the centroids

            Console.WriteLine("Final cluster centroids:");
            for (int i = 0; i < centroids.Length; ++i)
            {
                Console.WriteLine($"  Cluster {i}: " +
                    $"({centroids[i][0]:F3}, {centroids[i][1]:F3}, {centroids[i][2]:F3})");
            }

            Console.WriteLine("\nDone.");
            Console.ReadLine();
        }
    }
}