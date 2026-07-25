\- Implemented Lloyd’s algorithm with random initialization

\- Used squared Euclidean distance

\- Returns both centroids and cluster assignments

\- Based on Paulo Silva’s Medium article (Geek Culture)



Pseudocode:

function KMeans(dataset, k, maxIterations):

&#x20;   // Step 1

&#x20;   centroids ← select k random points from dataset



&#x20;   for iteration = 1 to maxIterations:

&#x20;       // Step 2 – Assignment

&#x20;       clusters ← empty list of k groups

&#x20;       for each point in dataset:

&#x20;           assign point to the nearest centroid (using squared Euclidean distance)



&#x20;       // Step 3 – Update

&#x20;       newCentroids ← empty list

&#x20;       for each cluster:

&#x20;           newCentroids.append( mean of all points in the cluster )



&#x20;       // Convergence check

&#x20;       if newCentroids ≈ centroids:

&#x20;           break



&#x20;       centroids ← newCentroids



&#x20;   return centroids, clusters





Formal \& technical explanation of how the code works

Algorithm family: Lloyd’s algorithm for K-Means clustering (1957 / 1982).

Objective function:

Minimize the Within-Cluster Sum of Squares (WCSS):

$$J = \\sum\_{i=1}^{k} \\sum\_{x \\in C\_i} \\| x - \\mu\_i \\|^2$$

where $  \\mu\_i  $ is the centroid of cluster $  C\_i  $.

Technical steps:



Initialization

Randomly sample $  k  $ distinct points from the dataset to serve as initial centroids $  \\mu\_1, \\dots, \\mu\_k  $.

Assignment Step (Expectation)

For every data point $  x  $, compute the squared Euclidean distance to all centroids and assign it to the cluster with the minimum distance:$$C\_i = \\{ x \\mid \\|x - \\mu\_i\\|^2 \\le \\|x - \\mu\_j\\|^2 \\ \\forall j \\}$$

Update Step (Maximization)

Recompute each centroid as the arithmetic mean of the points assigned to it:$$\\mu\_i = \\frac{1}{|C\_i|} \\sum\_{x \\in C\_i} x$$

Convergence

The algorithm stops when the centroids no longer change significantly (or a maximum number of iterations is reached). This guarantees that the objective function $  J  $ is non-increasing at every iteration.



Properties:



The algorithm always converges to a local minimum of WCSS.

It is sensitive to the initial centroids (hence the article later recommends Naive Sharding).

Time complexity per iteration is $  O(n \\cdot k \\cdot d)  $ where $  n  $ = number of points, $  k  $ = clusters, $  d  $ = dimensions.

