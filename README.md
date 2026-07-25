**K-Means Clustering for 3D Points using C# Sketch**

**Overview**
This is a clean, educational implementation of the \*\*K-Means clustering\*\* algorithm written in C#.

**Task:**  
Given an array of 3D points (arrays of 3 floating-point numbers), the algorithm returns the coordinates of the cluster centroids.

**Algorithm**
This implementation follows the classical **Lloyd’s algorithm**:
- **Initialization** – Randomly select *k* distinct points as initial centroids (Forgy method).
- **Assignment** – Assign every point to the nearest centroid using squared Euclidean distance.
- **Update** – Recalculate each centroid as the arithmetic mean of the points assigned to it.
- **Repeat** – Steps 2–3 until convergence or maximum iterations are reached.

Pseudocode
textfunction KMeans3D(points, k, maxIterations):
    // Step 1: Initialization (Forgy method)
    centroids ← select k distinct random points from points

    for iteration = 1 to maxIterations do
        changed ← false

        // Step 2: Assignment
        for each point p in points do
            bestCluster ← the centroid closest to p   // using Squared Euclidean distance
            if p was not already in bestCluster then
                assign p to bestCluster
                changed ← true
            end if
        end for

        if changed = false then
            break   // Converged – no point changed its cluster
        end if

        // Step 3: Update
        for each cluster c = 1 to k do
            if cluster c has at least one point then
                centroids[c] ← mean of all points assigned to cluster c
            else
                // Empty cluster → keep the previous centroid
            end if
        end for
    end for

    return centroids   // The final cluster coordinates

Explanation of the Pseudocode













































Part of PseudocodeWhat it doesWhy it is importantcentroids ← select k distinct random pointsChooses k different points randomly as starting centers (Forgy initialization)Gives the algorithm a starting pointbestCluster ← the centroid closest to pFinds the nearest centroid using Squared Euclidean distanceAssigns each point to the most similar clusterchanged ← trueMarks that at least one point moved to a different clusterUsed to detect convergenceif changed = false then breakStops the algorithm early when no points changed clustersPrevents unnecessary iterations (convergence check)centroids[c] ← mean of all points...Recalculates the center of each cluster as the average of its pointsMoves the centroids to better positionselse (empty cluster)Leaves the old centroid unchanged if a cluster has no pointsSimple and stable way to handle empty clustersreturn centroidsReturns the final positions of the cluster centersThis is exactly what the Itransition task requires
**In This Solution**
\- Follows the classical Lloyd algorithm described in all standard sources (Wikipedia, McCaffrey, StatQuest, etc.).
\- Input/output signature matches the requirement exactly.
\- Uses Euclidean distance (the mathematically correct metric for the usual WCSS objective).
\- Centroids are true arithmetic means.
\- Contains the usual practical safeguards (max iterations, convergence check, empty-cluster handling).
\- Based on a well-known educational implementation by James McCaffrey (Microsoft Research / Visual Studio Magazine).

**References** - See Other GitHub Branch
\- James McCaffrey – **K-Means Data Clustering from Scratch Using C#** (Visual Studio Magazine, Dec 2023)

\- Paulo Silva – **Implementing K-Means Clustering From Scratch in JavaScript** (Medium)

\- Wikipedia – K-means clustering

\- StatQuest – K-means clustering explanation

**How to Run**
1\. Create a new **Console App** in Visual Studio.

2\. Replace the content of **Program.cs** with the provided code.

3\. Press **F5** to run.

