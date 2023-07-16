// // // // using System.Linq;
// // // // using System;
// // // // using UnityEngine;
// // // // using System.Collections.Generic;
// // // // using static UnityEditor.Experimental.GraphView.GraphView;

// // // // public class VisHorizonChart1 : Vis
// // // // {
// // // //     public VisHorizonChart1()
// // // //     {
// // // //         title = "VisHorizon";
// // // //         dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
// // // //         tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
// // // //     }

// // // //     public override GameObject CreateVis(GameObject container)
// // // //     {
// // // //         base.CreateVis(container);

// // // //         // double bandHeight = 10.0f;
// // // //         double bandHeight = 0.01f;
// // // //         //double baseline = 0.006;
// // // //         double baseline = -0.01;
// // // //         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
// // // //         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
// // // //         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

// // // //         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight, baseline);
// // // //         double[] xValuesProcessed = xyValue.xValues;
// // // //         double[] yValuesProcessed = xyValue.yValues;
// // // //         int[] layeredValues = xyValue.layeredIndex;
// // // //         //## 01:  Create Axes and Grids
// // // //         // X Axis
// // // //         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
// // // //         visContainer.CreateGrid(Direction.X, Direction.Y);

// // // //         // Y Axis
// // // //         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
// // // //         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

// // // //         //## 02: Set Remaining Vis Channels (Color,...)
// // // //         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
// // // //         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
// // // //         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);

// // // //         double[] sizeList = new double[xValuesProcessed.Length];

// // // //         visContainer.SetChannel(VisChannel.XSize, sizeList);
// // // //         visContainer.SetChannel(VisChannel.YSize, sizeList);
// // // //         visContainer.SetChannel(VisChannel.ZSize, sizeList);
// // // //         //## 03: Draw all Data Points with the provided Channels 
// // // //         visContainer.CreateDataMarks(dataMarkPrefab);
// // // //         Draw(visContainer.dataMarkList, bandHeight, layeredValues);
// // // //         // ConnectPoints(visContainer.dataMarkList);
// // // //         //## 04: Rescale Chart
// // // //         visContainerObject.transform.localScale = new Vector3(width, height, depth);

// // // //         return visContainerObject;
// // // //     }

// // // //     public class xyValues
// // // //     {
// // // //         public double[] xValues { get; set; }
// // // //         public double[] yValues { get; set; }
// // // //         public int[] layeredIndex { get; set; }
// // // //     }

// // // //     private bool IsInTheBand(double point, double upLimit, double downLimit)
// // // //     {
// // // //         return point <= upLimit && point >= downLimit;
// // // //     }

// // // //     private double GetClosestBandLineLower(double yV, double bandHeight)
// // // //     {
// // // //         double closestBandLine = 0.0f;
// // // //         int numBandLine = 1;
// // // //         if (yV >= 0)
// // // //         {
// // // //             while (numBandLine * bandHeight <= yV)
// // // //             {
// // // //                 numBandLine += 1;
// // // //             }

// // // //             return (numBandLine - 1) * bandHeight;
// // // //         }
// // // //         else
// // // //         {
// // // //             while (numBandLine * bandHeight <= -yV)
// // // //             {
// // // //                 numBandLine += 1;
// // // //             }

// // // //             return (numBandLine - 1) * bandHeight * -1;
// // // //         }
// // // //     }

// // // //     private double GetClosestBandLineHigher(double yV, double bandHeight)
// // // //     {
// // // //         double closestBandLine = 0.0f;
// // // //         int numBandLine = 1;
// // // //         if (yV >= 0)
// // // //         {
// // // //             while (numBandLine * bandHeight <= yV)
// // // //             {
// // // //                 numBandLine += 1;
// // // //             }

// // // //             return numBandLine * bandHeight;
// // // //         }
// // // //         else
// // // //         {
// // // //             while (numBandLine * bandHeight <= -yV)
// // // //             {
// // // //                 numBandLine += 1;
// // // //             }

// // // //             return numBandLine * bandHeight * -1;
// // // //         }
// // // //     }

// // // //     private xyValues GetJunctionPoint(double[] oDataListX, double[] oDataListY, double bandHeight, double baseline)
// // // //     {
// // // //         // int numBands = 0;
// // // //         int numNegativeBands = 0;

// // // //         double previousPointX = 0.0;
// // // //         double previousPointY = 0.0;
// // // //         double Lower;
// // // //         double Higher;
// // // //         double tempMaxY = 0.0;
// // // //         double tempMinY = 0.0;
// // // //         double[] dataListX = new double[oDataListX.Length + 2];
// // // //         double[] dataListY = new double[oDataListY.Length + 2];

// // // //         dataListX[0] = oDataListX[0];
// // // //         dataListY[0] = 0.0 - baseline;

// // // //         for (int i = 0; i < oDataListY.Length; i++)
// // // //         {
// // // //             dataListY[i + 1] = oDataListY[i] - baseline;
// // // //             if (dataListY[i + 1] > tempMaxY)
// // // //             {
// // // //                 tempMaxY = dataListY[i + 1];
// // // //             }
// // // //             if (dataListY[i + 1] < tempMinY)
// // // //             {
// // // //                 tempMinY = dataListY[i + 1];
// // // //             }
// // // //         }
// // // //         dataListX[dataListX.Length - 1] = oDataListX[oDataListX.Length - 1];
// // // //         dataListY[dataListY.Length - 1] = 0.0 - baseline;

// // // //         numNegativeBands = (int)(Math.Abs(tempMinY) / bandHeight) + 1;
// // // //         // numBands = (Math.Abs(tempMinY) / bandHeight) + tempMaxY / bandHeight;

// // // //         previousPointX = dataListX[0];
// // // //         previousPointY = dataListY[0];
// // // //         List<double> xValueModified = new List<double>();
// // // //         List<double> yValueModified = new List<double>();
// // // //         List<int> layeredDataList = new List<int>();

// // // //         // double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
// // // //         // xValueModified.Add(dataListX[0]);
// // // //         // yValueModified.Add(Lower0);
// // // //         // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);
// // // //         // xValueModified.Add(dataListX[0]);
// // // //         // yValueModified.Add(dataListY[0]);
// // // //         // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);

// // // //         for (int i = 0; i < dataListX.Length; i++)
// // // //         {
// // // //             if (dataListY[i] >= 0)
// // // //             {
// // // //                 Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
// // // //                 Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
// // // //                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
// // // //                 {
// // // //                     if (dataListY[i] > previousPointY)
// // // //                     {
// // // //                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
// // // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // // //                         {
// // // //                             double tempLower = Lower - (j - 1) * bandHeight;
// // // //                             double tempHigher = Higher - (j - 1) * bandHeight;
// // // //                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
// // // //                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempLower);
// // // //                             layeredDataList.Add((int)(tempLower / bandHeight) - 1 + numNegativeBands);
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempLower);
// // // //                             layeredDataList.Add((int)(tempHigher / bandHeight) - 1 + numNegativeBands);
// // // //                         }
// // // //                     }
// // // //                     else if (dataListY[i] < previousPointY)
// // // //                     {
// // // //                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
// // // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // // //                         {
// // // //                             double tempLower = Lower + (j - 1) * bandHeight;
// // // //                             double tempHigher = Higher + (j - 1) * bandHeight;
// // // //                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
// // // //                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempHigher);
// // // //                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempHigher);
// // // //                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
// // // //                         }
// // // //                     }
// // // //                     xValueModified.Add(dataListX[i]);
// // // //                     yValueModified.Add(dataListY[i]);
// // // //                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
// // // //                 }
// // // //                 else
// // // //                 {
// // // //                     xValueModified.Add(dataListX[i]);
// // // //                     yValueModified.Add(dataListY[i]);
// // // //                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
// // // //                 }
// // // //                 previousPointX = dataListX[i];
// // // //                 previousPointY = dataListY[i];
// // // //             }
// // // //             else
// // // //             {
// // // //                 // negative number！！！！
// // // //                 Higher = GetClosestBandLineLower(dataListY[i], bandHeight);
// // // //                 Lower = GetClosestBandLineHigher(dataListY[i], bandHeight);
// // // //                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
// // // //                 {
// // // //                     if (dataListY[i] > previousPointY)
// // // //                     {
// // // //                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
// // // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // // //                         {
// // // //                             double tempLower = Lower - (j - 1) * bandHeight;
// // // //                             double tempHigher = Higher - (j - 1) * bandHeight;
// // // //                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
// // // //                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempLower);
// // // //                             layeredDataList.Add((int)((tempLower / bandHeight) + 1) * -1);
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempLower);
// // // //                             layeredDataList.Add((int)((tempHigher / bandHeight) + 1) * -1);
// // // //                         }
// // // //                     }
// // // //                     else if (dataListY[i] < previousPointY)
// // // //                     {
// // // //                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
// // // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // // //                         {
// // // //                             double tempLower = Lower + (j - 1) * bandHeight;
// // // //                             double tempHigher = Higher + (j - 1) * bandHeight;
// // // //                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
// // // //                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempHigher);
// // // //                             layeredDataList.Add((int)(tempHigher / bandHeight) * -1);
// // // //                             xValueModified.Add(xJunctionPoint);
// // // //                             yValueModified.Add(tempHigher);
// // // //                             layeredDataList.Add((int)(tempLower / bandHeight) * -1);
// // // //                         }
// // // //                     }
// // // //                     xValueModified.Add(dataListX[i]);
// // // //                     yValueModified.Add(dataListY[i]);
// // // //                     layeredDataList.Add((int)(Lower / bandHeight) * -1);
// // // //                 }
// // // //                 else
// // // //                 {
// // // //                     xValueModified.Add(dataListX[i]);
// // // //                     yValueModified.Add(dataListY[i]);
// // // //                     layeredDataList.Add((int)(Lower / bandHeight) * -1);
// // // //                 }
// // // //                 previousPointX = dataListX[i];
// // // //                 previousPointY = dataListY[i];
// // // //             }
// // // //         }
// // // //         // Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
// // // //         // xValueModified.Add(dataListX[dataListX.Length - 1]);
// // // //         // yValueModified.Add(Lower);
// // // //         // if (Lower >= 0)
// // // //         // {
// // // //         //     layeredDataList.Add((int)(Lower / bandHeight) - 1 + numNegativeBands);
// // // //         // }
// // // //         // else
// // // //         // {
// // // //         //     layeredDataList.Add((int)Math.Abs((Lower / bandHeight) - 1));
// // // //         // }
// // // //         return new xyValues { xValues = xValueModified.ToArray(), yValues = yValueModified.ToArray(), layeredIndex = layeredDataList.ToArray() };
// // // //     }

// // // //     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray)
// // // //     {
// // // //         int maxNumLayers = layeredValuesArray.Max() + 1;
// // // //         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
// // // //         layeredData = LayeringData(dataList, layeredValuesArray);
// // // //         ConnectLayerPoints(layeredData);
// // // //         CreateFillArea(layeredData);
// // // //     }

// // // //     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
// // // //     {
// // // //         List<List<Vector3>> layers = new List<List<Vector3>>();
// // // //         List<Vector3> originalPoints = new List<Vector3>();
// // // //         int numLayers = layeredValuesArray.Max() + 1;
// // // //         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

// // // //         foreach (var point in dataList)
// // // //         {
// // // //             var p = point.GetDataMarkChannel().position / 4.0f;
// // // //             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
// // // //             originalPoints.Add(pointOrigin);
// // // //         }

// // // //         for (int i = 0; i < numLayers; i++)
// // // //         {
// // // //             List<Vector3> layerPoints = new List<Vector3>();
// // // //             for (int j = 0; j < layeredValuesArray.Length; j++)
// // // //             {
// // // //                 if (layeredValuesArray[j] == i)
// // // //                 {
// // // //                     layerPoints.Add(originalPoints[j]);
// // // //                 }
// // // //             }
// // // //             List<Vector3> layerPointsTemp = new List<Vector3>(layerPoints);
// // // //             if (layerPointsTemp.Count > 0)
// // // //             {
// // // //                 float bottomLimit = layerPointsTemp[0].y;
// // // //                 for (int j = layerPointsTemp.Count - 2; j >= 0; j--)
// // // //                 {
// // // //                     layerPoints.Add(new Vector3(layerPointsTemp[j].x, bottomLimit, layerPointsTemp[j].z));
// // // //                 }
// // // //                 restructuredData[i] = layerPoints;
// // // //             }
// // // //             else
// // // //             {
// // // //                 restructuredData[i] = layerPoints;
// // // //             }
// // // //         }

// // // //         return restructuredData;
// // // //     }

// // // //     private void ConnectLayerPoints(List<Vector3>[] dataList)
// // // //     {
// // // //         foreach (var layer in dataList)
// // // //         {
// // // //             if (layer.Count != 0)
// // // //             {
// // // //                 ConnectLayerPoints(layer);
// // // //             }
// // // //         }
// // // //     }

// // // //     private void ConnectLayerPoints(List<Vector3> dataList)
// // // //     {
// // // //         GameObject lineObject = new GameObject("LineRenderer");
// // // //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
// // // //         // lineRenderer.transform.SetParent(container.transform);

// // // //         lineRenderer.positionCount = dataList.Count;
// // // //         lineRenderer.startWidth = 0.0002f;
// // // //         lineRenderer.endWidth = 0.0002f;
// // // //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// // // //         lineRenderer.SetPositions(dataList.ToArray());
// // // //     }

// // // //     private void CreateFillArea(List<Vector3>[] dataList)
// // // //     {
// // // //         string[] selectedcolors = new string[] { "blue", "red", "yellow", "green", "black" };
// // // //         for (int k = 0; k < dataList.Length; k++)
// // // //         {
// // // //             if (dataList[k].Count != 0)
// // // //             {
// // // //                 Vector3[] points = new Vector3[dataList[k].Count];
// // // //                 for (int i = 0; i < dataList[k].Count; i++)
// // // //                 {
// // // //                     points[i] = dataList[k][i] * 4.0f;
// // // //                 }
// // // //                 string selectedcolor = selectedcolors[k % 5];

// // // //                 GameObject fillAreaObject = new GameObject("FillArea");
// // // //                 fillAreaObject.transform.SetParent(visContainerObject.transform);
// // // //                 MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
// // // //                 MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
// // // //                 Mesh mesh = new Mesh();
// // // //                 mesh.vertices = points;
// // // //                 int[] triangles = new int[(points.Length - 1) * 3];
// // // //                 int index = 0;
// // // //                 for (int i = 0; i < points.Length - 1; i++)
// // // //                 {
// // // //                     triangles[index++] = i;
// // // //                     triangles[index++] = i + 1;
// // // //                     triangles[index++] = points.Length - i - 1;
// // // //                 }
// // // //                 mesh.triangles = triangles;
// // // //                 mesh.RecalculateNormals();
// // // //                 meshFilter.mesh = mesh;
// // // //                 meshRenderer.material = new Material(Shader.Find("Standard"));
// // // //                 switch (selectedcolor)
// // // //                 {
// // // //                     case "blue":
// // // //                         meshRenderer.material.color = Color.blue;
// // // //                         break;
// // // //                     case "red":
// // // //                         meshRenderer.material.color = Color.red;
// // // //                         break;
// // // //                     case "yellow":
// // // //                         meshRenderer.material.color = Color.yellow;
// // // //                         break;
// // // //                     case "green":
// // // //                         meshRenderer.material.color = Color.green;
// // // //                         break;
// // // //                     case "black":
// // // //                         meshRenderer.material.color = Color.black;
// // // //                         break;
// // // //                     default:
// // // //                         meshRenderer.material.color = Color.white;
// // // //                         break;
// // // //                 }
// // // //             }
// // // //         }
// // // //     }
// // // //     private void ConnectPoints(List<DataMark> dataList)
// // // //     {
// // // //         GameObject lineObject = new GameObject("LineRenderer");
// // // //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

// // // //         List<Vector3> points = new List<Vector3>();

// // // //         foreach (var point in dataList)
// // // //         {
// // // //             var p = point.GetDataMarkChannel().position / 4.0f;
// // // //             points.Add(p);
// // // //         }

// // // //         lineRenderer.positionCount = points.Count;
// // // //         lineRenderer.startWidth = 0.0002f;
// // // //         lineRenderer.endWidth = 0.0002f;
// // // //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// // // //         lineRenderer.SetPositions(points.ToArray());
// // // //     }
// // // // }


// // // using System.Linq;
// // // using System;
// // // using UnityEngine;
// // // using System.Collections.Generic;
// // // using static UnityEditor.Experimental.GraphView.GraphView;

// // // public class VisHorizonChart1 : Vis
// // // {
// // //     public VisHorizonChart1()
// // //     {
// // //         title = "VisHorizon";
// // //         dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
// // //         tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
// // //     }

// // //     public override GameObject CreateVis(GameObject container)
// // //     {
// // //         base.CreateVis(container);

// // //         // double bandHeight = 10.0f;
// // //         double bandHeight = 0.005f;
// // //         //double baseline = 0.006;
// // //         double baseline = 0.0185;
// // //         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
// // //         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
// // //         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

// // //         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight, baseline);
// // //         double[] xValuesProcessed = xyValue.xValues;
// // //         double[] yValuesProcessed = xyValue.yValues;
// // //         int[] layeredValues = xyValue.layeredIndex;
// // //         //## 01:  Create Axes and Grids
// // //         // X Axis
// // //         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
// // //         // visContainer.CreateGrid(Direction.X, Direction.Y);

// // //         // Y Axis
// // //         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
// // //         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

// // //         //## 02: Set Remaining Vis Channels (Color,...)
// // //         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
// // //         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
// // //         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
// // //         double[] sizeList = new double[xValuesProcessed.Length];

// // //         visContainer.SetChannel(VisChannel.XSize, sizeList);
// // //         visContainer.SetChannel(VisChannel.YSize, sizeList);
// // //         visContainer.SetChannel(VisChannel.ZSize, sizeList);
// // //         //## 03: Draw all Data Points with the provided Channels 
// // //         visContainer.CreateDataMarks(dataMarkPrefab);
// // //         Draw(visContainer.dataMarkList, bandHeight, layeredValues, xyValue.numNegativeBands);
// // //         // ConnectPoints(visContainer.dataMarkList);
// // //         //## 04: Rescale Chart
// // //         visContainerObject.transform.localScale = new Vector3(width, height, depth);

// // //         return visContainerObject;
// // //     }
    
// // //     public class xyValues
// // //     {
// // //         public double[] xValues { get; set; }
// // //         public double[] yValues { get; set; }
// // //         public int[] layeredIndex { get; set; }
// // //         public int numNegativeBands { get; set; }
// // //     }

// // //     private bool IsInTheBand(double point, double upLimit, double downLimit)
// // //     {
// // //         return point <= upLimit && point >= downLimit;
// // //     }
    
// // //     private double GetClosestBandLineLower(double yV, double bandHeight)
// // //     {
// // //         double closestBandLine = 0.0f;
// // //         int numBandLine = 1;
// // //         if (yV >= 0)
// // //         {
// // //             while (numBandLine * bandHeight <= yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return (numBandLine - 1) * bandHeight;
// // //         }
// // //         else
// // //         {
// // //             while (numBandLine * bandHeight <= -yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return (numBandLine - 1) * bandHeight * -1;
// // //         }
// // //     }

// // //     private double GetClosestBandLineHigher(double yV, double bandHeight)
// // //     {
// // //         double closestBandLine = 0.0f;
// // //         int numBandLine = 1;
// // //         if (yV >= 0)
// // //         {
// // //             while (numBandLine * bandHeight <= yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return numBandLine * bandHeight;
// // //         }
// // //         else
// // //         {
// // //              while (numBandLine * bandHeight <= -yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return numBandLine * bandHeight * -1;
// // //         }
// // //     }

// // //     private xyValues GetJunctionPoint(double[] oDataListX, double[] oDataListY, double bandHeight, double baseline)
// // //     {
// // //         // int numBands = 0;
// // //         int numNegativeBands = 0;

// // //         double previousPointX = 0.0;
// // //         double previousPointY = 0.0;
// // //         double Lower;
// // //         double Higher;
// // //         double tempMaxY = 0.0;
// // //         double tempMinY = 0.0;
// // //         double[] dataListX = new double[oDataListX.Length + 2];
// // //         double[] dataListY = new double[oDataListY.Length + 2];

// // //         dataListX[0] = oDataListX[0];
// // //         dataListY[0] = 0.0;

// // //         for (int i = 0; i < oDataListY.Length; i++)
// // //         {
// // //             dataListX[i + 1] = oDataListX[i];
// // //             dataListY[i + 1] = oDataListY[i] - baseline;
// // //             if (dataListY[i + 1] > tempMaxY)
// // //             {
// // //                 tempMaxY = dataListY[i + 1];
// // //             }
// // //             if (dataListY[i + 1] < tempMinY)
// // //             {
// // //                 tempMinY = dataListY[i + 1];
// // //             }
// // //         }
// // //         dataListX[dataListX.Length - 1] = oDataListX[oDataListX.Length - 1];
// // //         dataListY[dataListY.Length - 1] = 0.0;
        
// // //         numNegativeBands = (int)(Math.Abs(tempMinY) / bandHeight) + 1;
// // //         // numBands = (Math.Abs(tempMinY) / bandHeight) + tempMaxY / bandHeight;

// // //         previousPointX = dataListX[0];
// // //         previousPointY = dataListY[0];
// // //         List<double> xValueModified = new List<double>();
// // //         List<double> yValueModified = new List<double>();
// // //         List<int> layeredDataList = new List<int>();
        
// // //         // double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
// // //         // xValueModified.Add(dataListX[0]);
// // //         // yValueModified.Add(Lower0);
// // //         // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);
// // //         // xValueModified.Add(dataListX[0]);
// // //         // yValueModified.Add(dataListY[0]);
// // //         // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);

// // //         for (int i = 0; i < dataListX.Length; i++)
// // //         {
// // //             if (dataListY[i] >= 0)
// // //             {
// // //                 Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
// // //                 Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
// // //                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
// // //                 {
// // //                     if (dataListY[i] > previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower - (j - 1) * bandHeight;
// // //                             double tempHigher = Higher - (j - 1) * bandHeight;
// // //                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
// // //                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)(tempLower / bandHeight) - 1 + numNegativeBands);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)(tempHigher / bandHeight) - 1 + numNegativeBands);
// // //                         }
// // //                     }
// // //                     else if (dataListY[i] < previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower + (j - 1) * bandHeight;
// // //                             double tempHigher = Higher + (j - 1) * bandHeight;
// // //                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
// // //                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
// // //                         }
// // //                     }
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
// // //                 }
// // //                 else
// // //                 {
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
// // //                 }
// // //                 previousPointX = dataListX[i];
// // //                 previousPointY = dataListY[i];
// // //             }
// // //             else
// // //             {
// // //                 // negative number！！！！
// // //                 Higher = GetClosestBandLineLower(dataListY[i], bandHeight);
// // //                 Lower = GetClosestBandLineHigher(dataListY[i], bandHeight);
// // //                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
// // //                 {
// // //                     if (dataListY[i] > previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower - (j - 1) * bandHeight;
// // //                             double tempHigher = Higher - (j - 1) * bandHeight;
// // //                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
// // //                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)((tempLower / bandHeight)) + numNegativeBands - 1);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)((tempHigher / bandHeight)) + numNegativeBands - 1);
// // //                         }
// // //                     }
// // //                     else if (dataListY[i] < previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower + (j - 1) * bandHeight;
// // //                             double tempHigher = Higher + (j - 1) * bandHeight;
// // //                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
// // //                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
// // //                         }
// // //                     }
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
// // //                 }
// // //                 else
// // //                 {
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
// // //                 }
// // //                 previousPointX = dataListX[i];
// // //                 previousPointY = dataListY[i];
// // //             }
// // //         }
// // //         // Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
// // //         // xValueModified.Add(dataListX[dataListX.Length - 1]);
// // //         // yValueModified.Add(Lower);
// // //         // if (Lower >= 0)
// // //         // {
// // //         //     layeredDataList.Add((int)(Lower / bandHeight) - 1 + numNegativeBands);
// // //         // }
// // //         // else
// // //         // {
// // //         //     layeredDataList.Add((int)Math.Abs((Lower / bandHeight) - 1));
// // //         // }
// // //         return new xyValues { xValues = xValueModified.ToArray(), yValues = yValueModified.ToArray(), layeredIndex = layeredDataList.ToArray(), numNegativeBands = numNegativeBands };
// // //     }

// // //     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray, int numNegativeBands)
// // //     {
// // //         int maxNumLayers = layeredValuesArray.Max() + 1;
// // //         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
// // //         layeredData = LayeringData(dataList, layeredValuesArray);
// // //         // ConnectLayerPoints(layeredData);
// // //         CreateFillArea(layeredData, numNegativeBands);
// // //     }

// // //     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
// // //     {
// // //         List<List<Vector3>> layers = new List<List<Vector3>>();
// // //         List<Vector3> originalPoints = new List<Vector3>();
// // //         int numLayers = layeredValuesArray.Max() + 1;
// // //         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

// // //         foreach (var point in dataList)
// // //         {
// // //             var p = point.GetDataMarkChannel().position / 4.0f;

// // //             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);// 参考下面// if (k < numNegativeBands)那行开始修改
// // //             originalPoints.Add(pointOrigin);
// // //         }

// // //         for (int i = 0; i < numLayers; i++)
// // //         {
// // //             List<Vector3> layerPoints = new List<Vector3>();
// // //             for (int j = 0; j < layeredValuesArray.Length; j++)
// // //             {
// // //                 if (layeredValuesArray[j] == i)
// // //                 {
// // //                     layerPoints.Add(originalPoints[j]);
// // //                 }
// // //             }
// // //             List<Vector3> layerPointsTemp = new List<Vector3>(layerPoints);
// // //             if (layerPointsTemp.Count > 0)
// // //             {
// // //                 float bottomLimit = layerPointsTemp[0].y;
// // //                 for (int j = layerPointsTemp.Count - 2; j >= 0; j--)
// // //                 {
// // //                     layerPoints.Add(new Vector3(layerPointsTemp[j].x, bottomLimit, layerPointsTemp[j].z));
// // //                 }
// // //                 restructuredData[i] = layerPoints;
// // //             }
// // //             else
// // //             {
// // //                 restructuredData[i] = layerPoints;
// // //             }
// // //         }

// // //         return restructuredData;
// // //     }

// // //     private void ConnectLayerPoints(List<Vector3>[] dataList)
// // //     {
// // //         foreach (var layer in dataList)
// // //         {
// // //             if (layer.Count != 0)
// // //             {
// // //                 ConnectLayerPoints(layer);
// // //             }
// // //         }
// // //     }

// // //     private void ConnectLayerPoints(List<Vector3> dataList)
// // //     {
// // //         GameObject lineObject = new GameObject("LineRenderer");
// // //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
// // //         // lineRenderer.transform.SetParent(container.transform);
        
// // //         lineRenderer.positionCount = dataList.Count;
// // //         lineRenderer.startWidth = 0.0002f;
// // //         lineRenderer.endWidth = 0.0002f;
// // //         // lineRenderer.startWidth = 0.000f;
// // //         // lineRenderer.endWidth = 0.000f;
// // //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// // //         lineRenderer.SetPositions(dataList.ToArray());
// // //     }
    
// // //     private void CreateFillArea(List<Vector3>[] dataList, int numNegativeBands)
// // //     {
// // //         Color darkRed = new Color(139/255f, 0f, 0f, 65/240f);
// // //         Color lightRed = new Color(255/255f, 128/255f, 128/255f, 220/240f);
// // //         Color darkBlue = new Color(0f, 0f, 139/255f, 65/240f);
// // //         Color lightBlue = new Color(153/255f, 217/255f, 234/255f, 182/240f);
// // //         Color[] selectedColorsPositive = new Color[] {lightRed, darkRed};
// // //         Color[] selectedColorsNegative = new Color[] {darkBlue, lightBlue};
// // //         double maxV = double.NegativeInfinity;
// // //         double minV = double.PositiveInfinity;
// // //         double baseLinePosition = 0.0;
// // //         double modifiedHeightBand = dataList[1][0].y - dataList[0][0].y;
// // //         // double modifiedHeightBand = 0.0;
// // //         float pointX;
// // //         float pointY;
// // //         float pointZ;
// // //         float zDepth = 0.01f;
        
// // //         for (int listIndex = 0; listIndex < dataList.Length; listIndex++)
// // //         {
// // //             foreach (var V in dataList[listIndex])
// // //             {
// // //                 if (V.y > maxV)
// // //                 {
// // //                     maxV = V.y;
// // //                 }
// // //                 if (V.y < minV)
// // //                 {
// // //                     minV = V.y;
// // //                 }
// // //             }
// // //             if (listIndex == numNegativeBands)
// // //             {
// // //                 baseLinePosition = dataList[listIndex][0].y;
// // //             }
// // //         }

// // //         for (int k = 0; k < dataList.Length; k++)
// // //         {
// // //             if (dataList[k].Count != 0)
// // //             {
// // //                 Vector3[] points = new Vector3[dataList[k].Count];
// // //                 double maxLayerV = double.NegativeInfinity;
// // //                 double minLayerV = double.PositiveInfinity;
                
// // //                 for (int i = 0; i < dataList[k].Count; i++)
// // //                 {
// // //                     points[i] = dataList[k][i] * 4.0f;
// // //                     if (maxLayerV < points[i].y / 2.0f)
// // //                     {
// // //                         maxLayerV = points[i].y / 2.0f;
// // //                     }
// // //                     if (minLayerV > points[i].y / 2.0f)
// // //                     {
// // //                         minLayerV = points[i].y / 2.0f;
// // //                     }
// // //                 }
// // //                 if (k < numNegativeBands)
// // //                 {
// // //                     for (int i = 0; i < dataList[k].Count; i++)
// // //                     {
// // //                         pointX = dataList[k][i].x * 4.0f;
// // //                         pointY = (float)(dataList[k][i].y + modifiedHeightBand * (numNegativeBands - k - 1)) * 4.0f;
// // //                         pointZ = dataList[k][i].z * 4.0f + zDepth * k;
// // //                         points[i] = new Vector3(pointX, pointY, pointZ);
// // //                         if (maxLayerV < points[i].y / 2.0f)
// // //                         {
// // //                             maxLayerV = points[i].y / 2.0f;
// // //                         }
// // //                         if (minLayerV > points[i].y / 2.0f)
// // //                         {
// // //                             minLayerV = points[i].y / 2.0f;
// // //                         }
// // //                     }
// // //                 }
// // //                 else
// // //                 {
// // //                     for (int i = 0; i < dataList[k].Count; i++)
// // //                     {
                        
// // //                         pointX = dataList[k][i].x * 4.0f;
// // //                         pointY = (float)(dataList[k][i].y - modifiedHeightBand * (k - numNegativeBands)) * 4.0f;
// // //                         pointZ = dataList[k][i].z * 4.0f - zDepth * k;
// // //                         points[i] = new Vector3(pointX, pointY, pointZ);
// // //                         if (maxLayerV < points[i].y / 2.0f)
// // //                         {
// // //                             maxLayerV = points[i].y / 2.0f;
// // //                         }
// // //                         if (minLayerV > points[i].y / 2.0f)
// // //                         {
// // //                             minLayerV = points[i].y / 2.0f;
// // //                         }
// // //                     }
// // //                 }

// // //                 GameObject fillAreaObject = new GameObject("FillArea");
// // //                 fillAreaObject.transform.SetParent(visContainerObject.transform);
// // //                 MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
// // //                 MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
// // //                 Mesh mesh = new Mesh();
// // //                 mesh.vertices = points;
// // //                 int[] triangles = new int[(points.Length - 1) * 3];
// // //                 int index = 0;
// // //                 if (k < numNegativeBands)
// // //                 {
// // //                     for (int i = 0; i < points.Length - 1; i++)
// // //                     {
// // //                         triangles[index++] = points.Length - i - 1;
// // //                         triangles[index++] = i + 1;
// // //                         triangles[index++] = i;
// // //                     }
// // //                     meshRenderer.material = new Material(Shader.Find("Standard"));
// // //                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, minV, baseLinePosition, selectedColorsNegative);
// // //                 }
// // //                 else
// // //                 {
// // //                     for (int i = 0; i < points.Length - 1; i++)
// // //                     {
// // //                         triangles[index++] = i;
// // //                         triangles[index++] = i + 1;
// // //                         triangles[index++] = points.Length - i - 1;
// // //                     }
// // //                     meshRenderer.material = new Material(Shader.Find("Standard"));
// // //                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, baseLinePosition, maxV, selectedColorsPositive);
// // //                 }
// // //                 mesh.triangles = triangles;
// // //                 mesh.RecalculateNormals();
// // //                 // Debug.Log(mesh.normals[3]);
// // //                 meshFilter.mesh = mesh;
// // //             }
// // //         }
// // //     }

// // //     private void ConnectPoints(List<DataMark> dataList)
// // //     {
// // //         GameObject lineObject = new GameObject("LineRenderer");
// // //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        
// // //         List<Vector3> points = new List<Vector3>();

// // //         foreach (var point in dataList)
// // //         {
// // //             var p = point.GetDataMarkChannel().position / 4.0f;
// // //             points.Add(p);
// // //         }

// // //         lineRenderer.positionCount = points.Count;
// // //         lineRenderer.startWidth = 0.0002f;
// // //         lineRenderer.endWidth = 0.0002f;
// // //         // lineRenderer.startWidth = 0.000f;
// // //         // lineRenderer.endWidth = 0.000f;
// // //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// // //         lineRenderer.SetPositions(points.ToArray());
// // //     }
// // // }




// // // using System.Linq;
// // // using System;
// // // using UnityEngine;
// // // using System.Collections.Generic;
// // // using static UnityEditor.Experimental.GraphView.GraphView;

// // // public class VisHorizonChart : Vis
// // // {
// // //     public VisHorizonChart()
// // //     {
// // //         title = "VisHorizon";
// // //         dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
// // //         tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
// // //     }

// // //     public override GameObject CreateVis(GameObject container)
// // //     {
// // //         base.CreateVis(container);

// // //         // double bandHeight = 10.0f;
// // //         double bandHeight = 0.005f;
// // //         //double baseline = 0.006;
// // //         double baseline = 0.0235;
// // //         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
// // //         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
// // //         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

// // //         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight, baseline);
// // //         double[] xValuesProcessed = xyValue.xValues;
// // //         double[] yValuesProcessed = xyValue.yValues;
// // //         int[] layeredValues = xyValue.layeredIndex;
// // //         //## 01:  Create Axes and Grids
// // //         // X Axis
// // //         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
// // //         // visContainer.CreateGrid(Direction.X, Direction.Y);

// // //         // Y Axis
// // //         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
// // //         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

// // //         //## 02: Set Remaining Vis Channels (Color,...)
// // //         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
// // //         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
// // //         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
// // //         double[] sizeList = new double[xValuesProcessed.Length];

// // //         visContainer.SetChannel(VisChannel.XSize, sizeList);
// // //         visContainer.SetChannel(VisChannel.YSize, sizeList);
// // //         visContainer.SetChannel(VisChannel.ZSize, sizeList);
// // //         //## 03: Draw all Data Points with the provided Channels 
// // //         visContainer.CreateDataMarks(dataMarkPrefab);
// // //         Draw(visContainer.dataMarkList, bandHeight, layeredValues, xyValue.numNegativeBands, false, false);
// // //         // ConnectPoints(visContainer.dataMarkList);
// // //         //## 04: Rescale Chart
// // //         visContainerObject.transform.localScale = new Vector3(width, height, depth);

// // //         return visContainerObject;
// // //     }
    
// // //     public class xyValues
// // //     {
// // //         public double[] xValues { get; set; }
// // //         public double[] yValues { get; set; }
// // //         public int[] layeredIndex { get; set; }
// // //         public int numNegativeBands { get; set; }
// // //     }

// // //     private bool IsInTheBand(double point, double upLimit, double downLimit)
// // //     {
// // //         return point <= upLimit && point >= downLimit;
// // //     }
    
// // //     private double GetClosestBandLineLower(double yV, double bandHeight)
// // //     {
// // //         double closestBandLine = 0.0f;
// // //         int numBandLine = 1;
// // //         if (yV >= 0)
// // //         {
// // //             while (numBandLine * bandHeight <= yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return (numBandLine - 1) * bandHeight;
// // //         }
// // //         else
// // //         {
// // //             while (numBandLine * bandHeight <= -yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return (numBandLine - 1) * bandHeight * -1;
// // //         }
// // //     }

// // //     private double GetClosestBandLineHigher(double yV, double bandHeight)
// // //     {
// // //         double closestBandLine = 0.0f;
// // //         int numBandLine = 1;
// // //         if (yV >= 0)
// // //         {
// // //             while (numBandLine * bandHeight <= yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return numBandLine * bandHeight;
// // //         }
// // //         else
// // //         {
// // //              while (numBandLine * bandHeight <= -yV)
// // //             {
// // //                 numBandLine += 1;
// // //             }

// // //             return numBandLine * bandHeight * -1;
// // //         }
// // //     }

// // //     private xyValues GetJunctionPoint(double[] oDataListX, double[] oDataListY, double bandHeight, double baseline)
// // //     {
// // //         // int numBands = 0;
// // //         int numNegativeBands = 0;

// // //         double previousPointX = 0.0;
// // //         double previousPointY = 0.0;
// // //         double Lower;
// // //         double Higher;
// // //         double tempMaxY = 0.0;
// // //         double tempMinY = 0.0;
// // //         double[] dataListX = new double[oDataListX.Length + 2];
// // //         double[] dataListY = new double[oDataListY.Length + 2];

// // //         dataListX[0] = oDataListX[0];
// // //         dataListY[0] = 0.0;

// // //         for (int i = 0; i < oDataListY.Length; i++)
// // //         {
// // //             dataListX[i + 1] = oDataListX[i];
// // //             dataListY[i + 1] = oDataListY[i] - baseline;
// // //             if (dataListY[i + 1] > tempMaxY)
// // //             {
// // //                 tempMaxY = dataListY[i + 1];
// // //             }
// // //             if (dataListY[i + 1] < tempMinY)
// // //             {
// // //                 tempMinY = dataListY[i + 1];
// // //             }
// // //         }
// // //         dataListX[dataListX.Length - 1] = oDataListX[oDataListX.Length - 1];
// // //         dataListY[dataListY.Length - 1] = 0.0;
        
// // //         numNegativeBands = (int)(Math.Abs(tempMinY) / bandHeight) + 1;
// // //         // numBands = (Math.Abs(tempMinY) / bandHeight) + tempMaxY / bandHeight;

// // //         previousPointX = dataListX[0];
// // //         previousPointY = dataListY[0];
// // //         List<double> xValueModified = new List<double>();
// // //         List<double> yValueModified = new List<double>();
// // //         List<int> layeredDataList = new List<int>();
        
// // //         // double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
// // //         // xValueModified.Add(dataListX[0]);
// // //         // yValueModified.Add(Lower0);
// // //         // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);
// // //         // xValueModified.Add(dataListX[0]);
// // //         // yValueModified.Add(dataListY[0]);
// // //         // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);

// // //         for (int i = 0; i < dataListX.Length; i++)
// // //         {
// // //             if (dataListY[i] >= 0)
// // //             {
// // //                 Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
// // //                 Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
// // //                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
// // //                 {
// // //                     if (dataListY[i] > previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower - (j - 1) * bandHeight;
// // //                             double tempHigher = Higher - (j - 1) * bandHeight;
// // //                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
// // //                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)(tempLower / bandHeight) - 1 + numNegativeBands);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)(tempHigher / bandHeight) - 1 + numNegativeBands);
// // //                         }
// // //                     }
// // //                     else if (dataListY[i] < previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower + (j - 1) * bandHeight;
// // //                             double tempHigher = Higher + (j - 1) * bandHeight;
// // //                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
// // //                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
// // //                         }
// // //                     }
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
// // //                 }
// // //                 else
// // //                 {
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
// // //                 }
// // //                 previousPointX = dataListX[i];
// // //                 previousPointY = dataListY[i];
// // //             }
// // //             else
// // //             {
// // //                 // negative number！！！！
// // //                 Higher = GetClosestBandLineLower(dataListY[i], bandHeight);
// // //                 Lower = GetClosestBandLineHigher(dataListY[i], bandHeight);
// // //                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
// // //                 {
// // //                     if (dataListY[i] > previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower - (j - 1) * bandHeight;
// // //                             double tempHigher = Higher - (j - 1) * bandHeight;
// // //                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
// // //                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)((tempLower / bandHeight)) + numNegativeBands - 1);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempLower);
// // //                             layeredDataList.Add((int)((tempHigher / bandHeight)) + numNegativeBands - 1);
// // //                         }
// // //                     }
// // //                     else if (dataListY[i] < previousPointY)
// // //                     {
// // //                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
// // //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// // //                         {
// // //                             double tempLower = Lower + (j - 1) * bandHeight;
// // //                             double tempHigher = Higher + (j - 1) * bandHeight;
// // //                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
// // //                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
// // //                             xValueModified.Add(xJunctionPoint);
// // //                             yValueModified.Add(tempHigher);
// // //                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
// // //                         }
// // //                     }
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
// // //                 }
// // //                 else
// // //                 {
// // //                     xValueModified.Add(dataListX[i]);
// // //                     yValueModified.Add(dataListY[i]);
// // //                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
// // //                 }
// // //                 previousPointX = dataListX[i];
// // //                 previousPointY = dataListY[i];
// // //             }
// // //         }
// // //         // Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
// // //         // xValueModified.Add(dataListX[dataListX.Length - 1]);
// // //         // yValueModified.Add(Lower);
// // //         // if (Lower >= 0)
// // //         // {
// // //         //     layeredDataList.Add((int)(Lower / bandHeight) - 1 + numNegativeBands);
// // //         // }
// // //         // else
// // //         // {
// // //         //     layeredDataList.Add((int)Math.Abs((Lower / bandHeight) - 1));
// // //         // }
// // //         return new xyValues { xValues = xValueModified.ToArray(), yValues = yValueModified.ToArray(), layeredIndex = layeredDataList.ToArray(), numNegativeBands = numNegativeBands };
// // //     }

// // //     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray, int numNegativeBands, bool flagCompress, bool flagFlips)
// // //     {
// // //         int maxNumLayers = layeredValuesArray.Max() + 1;
// // //         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
// // //         layeredData = LayeringData(dataList, layeredValuesArray);
// // //         // ConnectLayerPoints(layeredData);
// // //         CreateFillArea(layeredData, numNegativeBands, flagCompress, flagFlips);
// // //     }

// // //     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
// // //     {
// // //         List<List<Vector3>> layers = new List<List<Vector3>>();
// // //         List<Vector3> originalPoints = new List<Vector3>();
// // //         int numLayers = layeredValuesArray.Max() + 1;
// // //         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

// // //         foreach (var point in dataList)
// // //         {
// // //             var p = point.GetDataMarkChannel().position / 4.0f;

// // //             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);// 参考下面// if (k < numNegativeBands)那行开始修改
// // //             originalPoints.Add(pointOrigin);
// // //         }

// // //         for (int i = 0; i < numLayers; i++)
// // //         {
// // //             List<Vector3> layerPoints = new List<Vector3>();
// // //             for (int j = 0; j < layeredValuesArray.Length; j++)
// // //             {
// // //                 if (layeredValuesArray[j] == i)
// // //                 {
// // //                     layerPoints.Add(originalPoints[j]);
// // //                 }
// // //             }
// // //             List<Vector3> layerPointsTemp = new List<Vector3>(layerPoints);
// // //             if (layerPointsTemp.Count > 0)
// // //             {
// // //                 float bottomLimit = layerPointsTemp[0].y;
// // //                 for (int j = layerPointsTemp.Count - 2; j >= 0; j--)
// // //                 {
// // //                     layerPoints.Add(new Vector3(layerPointsTemp[j].x, bottomLimit, layerPointsTemp[j].z));
// // //                 }
// // //                 restructuredData[i] = layerPoints;
// // //             }
// // //             else
// // //             {
// // //                 restructuredData[i] = layerPoints;
// // //             }
// // //         }

// // //         return restructuredData;
// // //     }

// // //     private void ConnectLayerPoints(List<Vector3>[] dataList)
// // //     {
// // //         foreach (var layer in dataList)
// // //         {
// // //             if (layer.Count != 0)
// // //             {
// // //                 ConnectLayerPoints(layer);
// // //             }
// // //         }
// // //     }

// // //     private void ConnectLayerPoints(List<Vector3> dataList)
// // //     {
// // //         GameObject lineObject = new GameObject("LineRenderer");
// // //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
// // //         // lineRenderer.transform.SetParent(container.transform);
        
// // //         lineRenderer.positionCount = dataList.Count;
// // //         lineRenderer.startWidth = 0.0002f;
// // //         lineRenderer.endWidth = 0.0002f;
// // //         // lineRenderer.startWidth = 0.000f;
// // //         // lineRenderer.endWidth = 0.000f;
// // //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// // //         lineRenderer.SetPositions(dataList.ToArray());
// // //     }
    
// // //     private void CreateFillArea(List<Vector3>[] dataList, int numNegativeBands, bool flagCompress, bool flagFlips)
// // //     {
// // //         Color darkRed = new Color(139/255f, 0f, 0f, 65/240f);
// // //         Color lightRed = new Color(255/255f, 128/255f, 128/255f, 220/240f);
// // //         Color darkBlue = new Color(0f, 0f, 139/255f, 65/240f);
// // //         Color lightBlue = new Color(153/255f, 217/255f, 234/255f, 182/240f);
// // //         Color[] selectedColorsPositive = new Color[] {lightRed, darkRed};
// // //         Color[] selectedColorsNegative = new Color[] {darkBlue, lightBlue};
// // //         double maxV = double.NegativeInfinity;
// // //         double minV = double.PositiveInfinity;
// // //         double baseLinePosition = 0.0;
// // //         double modifiedHeightBand;
// // //         if (flagCompress)
// // //         {
// // //             modifiedHeightBand = dataList[1][0].y - dataList[0][0].y;
// // //         }
// // //         else
// // //         {
// // //             modifiedHeightBand = 0.0;
// // //         }
// // //         float pointX;
// // //         float pointY;
// // //         float pointZ;
// // //         float zDepth = 0.00001f;
        
// // //         for (int listIndex = 0; listIndex < dataList.Length; listIndex++)
// // //         {
// // //             foreach (var V in dataList[listIndex])
// // //             {
// // //                 if (V.y > maxV)
// // //                 {
// // //                     maxV = V.y;
// // //                 }
// // //                 if (V.y < minV)
// // //                 {
// // //                     minV = V.y;
// // //                 }
// // //             }
// // //             if (listIndex == numNegativeBands)
// // //             {
// // //                 baseLinePosition = dataList[listIndex][0].y;
// // //             }
// // //         }
// // //         double modifiedBaseLine = dataList[0][0].y * 2.0f;

// // //         for (int k = 0; k < dataList.Length; k++)
// // //         {
// // //             if (dataList[k].Count != 0)
// // //             {
// // //                 Vector3[] points = new Vector3[dataList[k].Count];
// // //                 double maxLayerV = 0.0;
// // //                 double minLayerV = 0.0;
                
// // //                 for (int kk = 0; kk < dataList[k].Count; kk++)
// // //                 {
// // //                     points[kk] = dataList[k][kk] * 4.0f;
// // //                     if (maxLayerV < points[kk].y / 2.0f)
// // //                     {
// // //                         maxLayerV = points[kk].y / 2.0f;
// // //                     }
// // //                     if (minLayerV > points[kk].y / 2.0f)
// // //                     {
// // //                         minLayerV = points[kk].y / 2.0f;
// // //                     }
// // //                 }
// // //                 if (k < numNegativeBands && !flagFlips)
// // //                 {
// // //                     for (int kk = 0; kk < dataList[k].Count; kk++)
// // //                     {
// // //                         pointX = dataList[k][kk].x * 4.0f;
// // //                         // pointY = (float)(dataList[k][kk].y + modifiedHeightBand * (numNegativeBands - k - 1)) * 4.0f;
// // //                         pointY = dataList[k][kk].y * 4.0f;
// // //                         pointZ = dataList[k][kk].z * 4.0f + zDepth * k;
// // //                         points[kk] = new Vector3(pointX, pointY, pointZ);
// // //                         if (maxLayerV < points[kk].y / 4.0f)
// // //                         {
// // //                             maxLayerV = points[kk].y / 4.0f;
// // //                         }
// // //                         if (minLayerV > points[kk].y / 4.0f)
// // //                         {
// // //                             minLayerV = points[kk].y / 4.0f;
// // //                         }
// // //                     }
// // //                 }
// // //                 else if (k < numNegativeBands && flagFlips)
// // //                 {
// // //                     Debug.Log("flips");
// // //                     for (int kk = 0; kk < dataList[k].Count; kk++)
// // //                     {
// // //                         pointX = dataList[k][kk].x * 4.0f;
// // //                         // pointY = 2.0f * (float)modifiedBaseLine + (float)(dataList[k][kk].y + modifiedHeightBand * (numNegativeBands - k - 1)) * 4.0f;
// // //                         pointY = dataList[k][kk].y * 4.0f;
// // //                         pointZ = dataList[k][kk].z * 4.0f + zDepth * k;
// // //                         points[kk] = new Vector3(pointX, pointY, pointZ);
// // //                         if (maxLayerV < points[kk].y / 4.0f)
// // //                         {
// // //                             maxLayerV = points[kk].y / 4.0f;
// // //                         }
// // //                         if (minLayerV > points[kk].y / 4.0f)
// // //                         {
// // //                             minLayerV = points[kk].y / 4.0f;
// // //                         }
// // //                     }
// // //                 }
// // //                 else
// // //                 {
// // //                     for (int kk = 0; kk < dataList[k].Count; kk++)
// // //                     {
// // //                         pointX = dataList[k][kk].x * 4.0f;
// // //                         // pointY = (float)(dataList[k][kk].y - modifiedHeightBand * (k - numNegativeBands)) * 4.0f;
// // //                         pointY = dataList[k][kk].y * 4.0f;
// // //                         pointZ = dataList[k][kk].z * 4.0f - zDepth * k;
// // //                         points[kk] = new Vector3(pointX, pointY, pointZ);
// // //                         if (maxLayerV < points[kk].y / 4.0f)
// // //                         {
// // //                             maxLayerV = points[kk].y / 4.0f;
// // //                         }
// // //                         if (minLayerV > points[kk].y / 4.0f)
// // //                         {
// // //                             minLayerV = points[kk].y / 4.0f;
// // //                         }
// // //                     }
// // //                 }

// // //                 GameObject fillAreaObject = new GameObject("FillArea");
// // //                 fillAreaObject.transform.SetParent(visContainerObject.transform);
// // //                 MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
// // //                 MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
// // //                 Mesh mesh = new Mesh();
// // //                 mesh.vertices = points;
// // //                 int[] triangles = new int[(points.Length - 1) * 3];
// // //                 int index = 0;
// // //                 if (k < numNegativeBands)
// // //                 {
// // //                     for (int i = 0; i < points.Length - 1; i++)
// // //                     {
// // //                         triangles[index++] = points.Length - i - 1;
// // //                         triangles[index++] = i + 1;
// // //                         triangles[index++] = i;
// // //                     }
// // //                     meshRenderer.material = new Material(Shader.Find("Standard"));
// // //                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, minV, baseLinePosition, selectedColorsNegative);
// // //                 }
// // //                 else
// // //                 {
// // //                     for (int i = 0; i < points.Length - 1; i++)
// // //                     {
// // //                         triangles[index++] = i;
// // //                         triangles[index++] = i + 1;
// // //                         triangles[index++] = points.Length - i - 1;
// // //                     }
// // //                     meshRenderer.material = new Material(Shader.Find("Standard"));
// // //                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, baseLinePosition, maxV, selectedColorsPositive);
// // //                 }
// // //                 mesh.triangles = triangles;
// // //                 mesh.RecalculateNormals();
// // //                 // Debug.Log(mesh.normals[3]);
// // //                 meshFilter.mesh = mesh;
// // //             }
// // //         }
// // //     }

// // //     private void ConnectPoints(List<DataMark> dataList)
// // //     {
// // //         GameObject lineObject = new GameObject("LineRenderer");
// // //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        
// // //         List<Vector3> points = new List<Vector3>();

// // //         foreach (var point in dataList)
// // //         {
// // //             var p = point.GetDataMarkChannel().position / 4.0f;
// // //             points.Add(p);
// // //         }

// // //         lineRenderer.positionCount = points.Count;
// // //         lineRenderer.startWidth = 0.0002f;
// // //         lineRenderer.endWidth = 0.0002f;
// // //         // lineRenderer.startWidth = 0.000f;
// // //         // lineRenderer.endWidth = 0.000f;
// // //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// // //         lineRenderer.SetPositions(points.ToArray());
// // //     }
// // // }


// // previousPointY - Higher) / bandHeight);
// //                         for (int j = timeAddJunctionPoint; j > 0; j--)
// //                         {
// //                             double tempLower = Lower + (j - 1) * bandHeight;
// //                             double tempHigher = Higher + (j - 1) * bandHeight;
// //                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
// //                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
// //                             xValueModified.Add(xJunctionPoint);
// //                             yValueModified.Add(tempHigher);
// //                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
// //                             xValueModified.Add(xJunctionPoint);
// //                             yValueModified.Add(tempHigher);
// //                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
// //                         }
// //                     }
// //                     xValueModified.Add(dataListX[i]);
// //                     yValueModified.Add(dataListY[i]);
// //                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
// //                 }
// //                 else
// //                 {
// //                     xValueModified.Add(dataListX[i]);
// //                     yValueModified.Add(dataListY[i]);
// //                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
// //                 }
// //                 previousPointX = dataListX[i];
// //                 previousPointY = dataListY[i];
// //             }
// //         }
// //         return new xyValues { xValues = xValueModified.ToArray(), yValues = yValueModified.ToArray(), layeredIndex = layeredDataList.ToArray(), numNegativeBands = numNegativeBands };
// //     }

// //     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray, int numNegativeBands, bool flagCompress, bool flagFlips)
// //     {
// //         int maxNumLayers = layeredValuesArray.Max() + 1;
// //         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
// //         layeredData = LayeringData(dataList, layeredValuesArray);
// //         // ConnectLayerPoints(layeredData);
// //         CreateFillArea(layeredData, numNegativeBands, flagCompress, flagFlips);
// //     }

// //     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
// //     {
// //         List<List<Vector3>> layers = new List<List<Vector3>>();
// //         List<Vector3> originalPoints = new List<Vector3>();
// //         int numLayers = layeredValuesArray.Max() + 1;
// //         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

// //         foreach (var point in dataList)
// //         {
// //             var p = point.GetDataMarkChannel().position / 4.0f;

// //             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);// 参考下面// if (k < numNegativeBands)那行开始修改
// //             originalPoints.Add(pointOrigin);
// //         }

// //         for (int i = 0; i < numLayers; i++)
// //         {
// //             List<Vector3> layerPoints = new List<Vector3>();
// //             for (int j = 0; j < layeredValuesArray.Length; j++)
// //             {
// //                 if (layeredValuesArray[j] == i)
// //                 {
// //                     layerPoints.Add(originalPoints[j]);
// //                 }
// //             }
// //             List<Vector3> layerPointsTemp = new List<Vector3>(layerPoints);
// //             if (layerPointsTemp.Count > 0)
// //             {
// //                 float bottomLimit = layerPointsTemp[0].y;
// //                 for (int j = layerPointsTemp.Count - 2; j >= 0; j--)
// //                 {
// //                     layerPoints.Add(new Vector3(layerPointsTemp[j].x, bottomLimit, layerPointsTemp[j].z));
// //                 }
// //                 restructuredData[i] = layerPoints;
// //             }
// //             else
// //             {
// //                 restructuredData[i] = layerPoints;
// //             }
// //         }

// //         return restructuredData;
// //     }

// //     private void ConnectLayerPoints(List<Vector3>[] dataList)
// //     {
// //         foreach (var layer in dataList)
// //         {
// //             if (layer.Count != 0)
// //             {
// //                 ConnectLayerPoints(layer);
// //             }
// //         }
// //     }

// //     private void ConnectLayerPoints(List<Vector3> dataList)
// //     {
// //         GameObject lineObject = new GameObject("LineRenderer");
// //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
// //         // lineRenderer.transform.SetParent(container.transform);
        
// //         lineRenderer.positionCount = dataList.Count;
// //         lineRenderer.startWidth = 0.0002f;
// //         lineRenderer.endWidth = 0.0002f;
// //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// //         lineRenderer.SetPositions(dataList.ToArray());
// //     }
    
// //     private void CreateFillArea(List<Vector3>[] dataList, int numNegativeBands, bool flagCompress, bool flagFlips)
// //     {
// //         Color darkRed = new Color(139/255f, 0f, 0f, 65/240f);
// //         Color lightRed = new Color(255/255f, 128/255f, 128/255f, 220/240f);
// //         Color darkBlue = new Color(0f, 0f, 139/255f, 65/240f);
// //         Color lightBlue = new Color(153/255f, 217/255f, 234/255f, 182/240f);
// //         Color[] selectedColorsPositive = new Color[] {lightRed, darkRed};
// //         Color[] selectedColorsNegative = new Color[] {darkBlue, lightBlue};
// //         Color[] selectedColorsNegativeReversed = (Color[])selectedColorsNegative.Clone();
// //         Array.Reverse(selectedColorsNegativeReversed);
// //         double maxV = double.NegativeInfinity;
// //         double minV = double.PositiveInfinity;
// //         double baseLinePosition = 0.0;
// //         double modifiedHeightBand;
// //         if (flagCompress)
// //         {
// //             // modifiedHeightBand = dataList[numNegativeBands - 1][0].y - dataList[0][0].y;
// //             modifiedHeightBand = dataList[1][0].y - dataList[0][0].y;
// //         }
// //         else
// //         {
// //             modifiedHeightBand = 0.0;
// //         }
// //         float pointX;
// //         float pointY;
// //         float pointZ;
// //         float zDepth = 0.00001f;
        
// //         for (int listIndex = 0; listIndex < dataList.Length; listIndex++)
// //         {
// //             foreach (var V in dataList[listIndex])
// //             {
// //                 if (V.y * 4.0f > maxV)
// //                 {
// //                     maxV = V.y * 4.0f;
// //                 }
// //                 if (V.y * 4.0f < minV)
// //                 {
// //                     minV = V.y * 4.0f;
// //                 }
// //             }
// //             if (listIndex == numNegativeBands)
// //             {
// //                 baseLinePosition = dataList[listIndex][0].y;
// //             }
// //         }
// //         double modifiedBaseLine = dataList[numNegativeBands][0].y * 4.0f;

// //         for (int k = 0; k < dataList.Length; k++)
// //         {
// //             if (dataList[k].Count != 0)
// //             {
// //                 Vector3[] points = new Vector3[dataList[k].Count];
// //                 double maxLayerV = double.NegativeInfinity;
// //                 double minLayerV = double.PositiveInfinity;
// //                 if (k < numNegativeBands && !flagFlips)
// //                 {
// //                     for (int kk = 0; kk < dataList[k].Count; kk++)
// //                     {
// //                         pointX = dataList[k][kk].x * 4.0f;
// //                         pointY = (float)(dataList[k][kk].y + modifiedHeightBand * (numNegativeBands - k - 1)) * 4.0f;
// //                         pointZ = dataList[k][kk].z * 4.0f + zDepth * k;
// //                         points[kk] = new Vector3(pointX, pointY, pointZ);
// //                         if (maxLayerV < dataList[k][kk].y * 4.0f)
// //                         {
// //                             maxLayerV = dataList[k][kk].y * 4.0f;
// //                         }
// //                         if (minLayerV > dataList[k][kk].y * 4.0f)
// //                         {
// //                             minLayerV = dataList[k][kk].y * 4.0f;
// //                         }
// //                     }
// //                 }
// //                 else if (k < numNegativeBands && flagFlips)
// //                 {
// //                     for (int kk = 0; kk < dataList[k].Count; kk++)
// //                     {
// //                         pointX = dataList[k][kk].x * 4.0f;
// //                         pointY = 2.0f * (float)modifiedBaseLine - (float)(dataList[k][kk].y + modifiedHeightBand * (numNegativeBands - k - 1)) * 4.0f;
// //                         pointZ = dataList[k][kk].z * 4.0f + zDepth * k;
// //                         points[kk] = new Vector3(pointX, pointY, pointZ);
// //                         if (maxLayerV < dataList[k][kk].y * 4.0f)
// //                         {
// //                             maxLayerV = dataList[k][kk].y * 4.0f;
// //                         }
// //                         if (minLayerV > dataList[k][kk].y * 4.0f)
// //                         {
// //                             minLayerV = dataList[k][kk].y * 4.0f;
// //                         }
// //                     }
// //                 }
// //                 else
// //                 {
// //                     for (int kk = 0; kk < dataList[k].Count; kk++)
// //                     {
// //                         pointX = dataList[k][kk].x * 4.0f;
// //                         pointY = (float)(dataList[k][kk].y - modifiedHeightBand * (k - numNegativeBands)) * 4.0f;
// //                         pointZ = dataList[k][kk].z * 4.0f - zDepth * k;
// //                         points[kk] = new Vector3(pointX, pointY, pointZ);
// //                         if (maxLayerV < dataList[k][kk].y * 4.0f)
// //                         {
// //                             maxLayerV = dataList[k][kk].y * 4.0f;
// //                         }
// //                         if (minLayerV > dataList[k][kk].y * 4.0f)
// //                         {
// //                             minLayerV = dataList[k][kk].y * 4.0f;
// //                         }
// //                     }
// //                 }

// //                 GameObject fillAreaObject = new GameObject("FillArea");
// //                 fillAreaObject.transform.SetParent(visContainerObject.transform);
// //                 MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
// //                 MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
// //                 Mesh mesh = new Mesh();
// //                 mesh.vertices = points;
// //                 int[] triangles = new int[(points.Length - 1) * 3];
// //                 int index = 0;
// //                 if (k < numNegativeBands && !flagFlips)
// //                 {
// //                     for (int i = 0; i < points.Length - 1; i++)
// //                     {
// //                         triangles[index++] = points.Length - i - 1;
// //                         triangles[index++] = i + 1;
// //                         triangles[index++] = i;
// //                     }
// //                     meshRenderer.material = new Material(Shader.Find("Standard"));
// //                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, minV, modifiedBaseLine, selectedColorsNegative);
// //                 }
// //                 else if (k < numNegativeBands && flagFlips)
// //                 {
// //                     for (int i = 0; i < points.Length - 1; i++)
// //                     {
// //                         triangles[index++] = i;
// //                         triangles[index++] = i + 1;
// //                         triangles[index++] = points.Length - i - 1;
// //                     }
// //                     meshRenderer.material = new Material(Shader.Find("Standard"));
// //                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, minV, modifiedBaseLine, selectedColorsNegative);
// //                 }
// //                 else
// //                 {
// //                     for (int i = 0; i < points.Length - 1; i++)
// //                     {
// //                         triangles[index++] = i;
// //                         triangles[index++] = i + 1;
// //                         triangles[index++] = points.Length - i - 1;
// //                     }
// //                     meshRenderer.material = new Material(Shader.Find("Standard"));
// //                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, modifiedBaseLine, maxV, selectedColorsPositive);
// //                 }
// //                 mesh.triangles = triangles;
// //                 mesh.RecalculateNormals();
// //                 // Debug.Log(mesh.normals[3]);
// //                 meshFilter.mesh = mesh;
// //             }
// //         }
// //     }

// //     private void ConnectPoints(List<DataMark> dataList)
// //     {
// //         GameObject lineObject = new GameObject("LineRenderer");
// //         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        
// //         List<Vector3> points = new List<Vector3>();

// //         foreach (var point in dataList)
// //         {
// //             var p = point.GetDataMarkChannel().position / 4.0f;
// //             points.Add(p);
// //         }

// //         lineRenderer.positionCount = points.Count;
// //         lineRenderer.startWidth = 0.0002f;
// //         lineRenderer.endWidth = 0.0002f;
// //         // lineRenderer.startWidth = 0.000f;
// //         // lineRenderer.endWidth = 0.000f;
// //         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

// //         lineRenderer.SetPositions(points.ToArray());
// //     }
// // }










// using System.Linq;
// using System;
// using UnityEngine;
// using System.Collections.Generic;
// using static UnityEditor.Experimental.GraphView.GraphView;

// public class VisHorizonChart : Vis
// {
//     public VisHorizonChart()
//     {
//         title = "VisHorizon";
//         dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
//         tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
//     }

//     public override GameObject CreateVis(GameObject container)
//     {
//         base.CreateVis(container);

//         // double bandHeight = 0.027f;
//         // double baseline = 0.0714;
//         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
//         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
//         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();
        
//         double bandHeight = 0.01f;
//         // double baseline = 0.0714;
//         double baseline = GetProperHeight(yValues);

//         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight, baseline);
//         double[] xValuesProcessed = xyValue.xValues;
//         double[] yValuesProcessed = xyValue.yValues;
//         int[] layeredValues = xyValue.layeredIndex;
//         //## 01:  Create Axes and Grids
//         // X Axis
//         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
//         // visContainer.CreateGrid(Direction.X, Direction.Y);

//         // Y Axis
//         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
//         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

//         //## 02: Set Remaining Vis Channels (Color,...)
//         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
//         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
//         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
//         double[] sizeList = new double[xValuesProcessed.Length];

//         visContainer.SetChannel(VisChannel.XSize, sizeList);
//         visContainer.SetChannel(VisChannel.YSize, sizeList);
//         visContainer.SetChannel(VisChannel.ZSize, sizeList);
//         //## 03: Draw all Data Points with the provided Channels 
//         visContainer.CreateDataMarks(dataMarkPrefab);
//         Draw(visContainer.dataMarkList, bandHeight, layeredValues, xyValue.numNegativeBands, true, true);
//         // ConnectPoints(visContainer.dataMarkList);
//         //## 04: Rescale Chart
//         visContainerObject.transform.localScale = new Vector3(width, height, depth);

//         return visContainerObject;
//     }

//     private double GetProperHeight(double[] points)
//     {
//         double maxHeight = double.NegativeInfinity;
//         double minHeight = double.PositiveInfinity;
        
//         foreach (var point in points)
//         {
//             if (point > maxHeight)
//             {
//                 maxHeight = point;
//             }
//             if (point < minHeight)
//             {
//                 minHeight = point;
//             }
//         }

//         return (maxHeight + minHeight) / 2.0;
//     }
    
//     public class xyValues
//     {
//         public double[] xValues { get; set; }
//         public double[] yValues { get; set; }
//         public int[] layeredIndex { get; set; }
//         public int numNegativeBands { get; set; }
//     }

//     private bool IsInTheBand(double point, double upLimit, double downLimit)
//     {
//         return point <= upLimit && point >= downLimit;
//     }
    
//     private double GetClosestBandLineLower(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         if (yV > 0)
//         {
//             while (numBandLine * bandHeight <= yV)
//             {
//                 numBandLine += 1;
//             }

//             return (numBandLine - 1) * bandHeight;
//         }
//         else
//         {
//             while (numBandLine * bandHeight <= -yV)
//             {
//                 numBandLine += 1;
//             }

//             return numBandLine * bandHeight * -1;
//         }
//     }

//     private double GetClosestBandLineHigher(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         if (yV > 0)
//         {
//             while (numBandLine * bandHeight <= yV)
//             {
//                 numBandLine += 1;
//             }

//             return numBandLine * bandHeight;
//         }
//         else
//         {
//              while (numBandLine * bandHeight <= -yV)
//             {
//                 numBandLine += 1;
//             }

//             return (numBandLine - 1) * bandHeight * -1;
//         }
//     }

//     private xyValues GetJunctionPoint(double[] oDataListX, double[] oDataListY, double bandHeight, double baseline)
//     {
//         // int numBands = 0;
//         int numNegativeBands = 0;

//         double previousPointX = 0.0;
//         double previousPointY = 0.0;
//         double Lower;
//         double Higher;
//         double tempMaxY = double.NegativeInfinity;
//         double tempMinY = double.PositiveInfinity;
//         double[] dataListX = new double[oDataListX.Length + 2];
//         double[] dataListY = new double[oDataListY.Length + 2];

//         dataListX[0] = oDataListX[0];
//         dataListY[0] = 0.0;

//         for (int i = 0; i < oDataListY.Length; i++)
//         {
//             dataListX[i + 1] = oDataListX[i];
//             dataListY[i + 1] = oDataListY[i] - baseline;
//             if (dataListY[i + 1] > tempMaxY)
//             {
//                 tempMaxY = dataListY[i + 1];
//             }
//             if (dataListY[i + 1] < tempMinY)
//             {
//                 tempMinY = dataListY[i + 1];
//             }
//         }
//         dataListX[dataListX.Length - 1] = oDataListX[oDataListX.Length - 1];
//         dataListY[dataListY.Length - 1] = 0.0;
        
//         numNegativeBands = (int)(Math.Abs(tempMinY) / bandHeight) + 1;
//         // numBands = (Math.Abs(tempMinY) / bandHeight) + tempMaxY / bandHeight;

//         previousPointX = dataListX[0];
//         previousPointY = dataListY[0];
//         List<double> xValueModified = new List<double>();
//         List<double> yValueModified = new List<double>();
//         List<int> layeredDataList = new List<int>();

//         for (int i = 0; i < dataListX.Length; i++)
//         {
//             if (dataListY[i] >= 0)
//             {
//                 Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//                 Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
//                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//                 {
//                     if (dataListY[i] > previousPointY)
//                     {
//                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
//                         for (int j = timeAddJunctionPoint; j > 0; j--)
//                         {
//                             double tempLower = Lower - (j - 1) * bandHeight;
//                             double tempHigher = Higher - (j - 1) * bandHeight;
//                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
//                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempLower);
//                             layeredDataList.Add((int)(tempLower / bandHeight) - 1 + numNegativeBands);
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempLower);
//                             layeredDataList.Add((int)(tempHigher / bandHeight) - 1 + numNegativeBands);
//                         }
//                     }
//                     else if (dataListY[i] < previousPointY)
//                     {
//                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
//                         for (int j = timeAddJunctionPoint; j > 0; j--)
//                         {
//                             double tempLower = Lower + (j - 1) * bandHeight;
//                             double tempHigher = Higher + (j - 1) * bandHeight;
//                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
//                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempHigher);
//                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempHigher);
//                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
//                         }
//                     }
//                     xValueModified.Add(dataListX[i]);
//                     yValueModified.Add(dataListY[i]);
//                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
//                 }
//                 else
//                 {
//                     xValueModified.Add(dataListX[i]);
//                     yValueModified.Add(dataListY[i]);
//                     layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
//                 }
//                 previousPointX = dataListX[i];
//                 previousPointY = dataListY[i];
//             }
//             else
//             {
//                 // negative number！！！！
//                 Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//                 Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
//                 if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//                 {
//                     if (dataListY[i] > previousPointY)
//                     {
//                         int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
//                         for (int j = timeAddJunctionPoint; j > 0; j--)
//                         {
//                             double tempLower = Lower - (j - 1) * bandHeight;
//                             double tempHigher = Higher - (j - 1) * bandHeight;
//                             double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
//                             double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempLower);
//                             layeredDataList.Add((int)((tempLower / bandHeight)) + numNegativeBands - 1);
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempLower);
//                             layeredDataList.Add((int)((tempHigher / bandHeight)) + numNegativeBands - 1);
//                         }
//                     }
//                     else if (dataListY[i] < previousPointY)
//                     {
//                         int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
//                         for (int j = timeAddJunctionPoint; j > 0; j--)
//                         {
//                             double tempLower = Lower + (j - 1) * bandHeight;
//                             double tempHigher = Higher + (j - 1) * bandHeight;
//                             double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
//                             double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempHigher);
//                             layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
//                             xValueModified.Add(xJunctionPoint);
//                             yValueModified.Add(tempHigher);
//                             layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
//                         }
//                     }
//                     xValueModified.Add(dataListX[i]);
//                     yValueModified.Add(dataListY[i]);
//                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
//                 }
//                 else
//                 {
//                     xValueModified.Add(dataListX[i]);
//                     yValueModified.Add(dataListY[i]);
//                     layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
//                 }
//                 previousPointX = dataListX[i];
//                 previousPointY = dataListY[i];
//             }
//         }
//         return new xyValues { xValues = xValueModified.ToArray(), yValues = yValueModified.ToArray(), layeredIndex = layeredDataList.ToArray(), numNegativeBands = numNegativeBands };
//     }

//     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray, int numNegativeBands, bool flagCompress, bool flagFlips)
//     {
//         int maxNumLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
//         layeredData = LayeringData(dataList, layeredValuesArray);
//         // ConnectLayerPoints(layeredData);
//         CreateFillArea(layeredData, numNegativeBands, flagCompress, flagFlips);
//     }

//     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
//     {
//         List<List<Vector3>> layers = new List<List<Vector3>>();
//         List<Vector3> originalPoints = new List<Vector3>();
//         int numLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

//         foreach (var point in dataList)
//         {
//             var p = point.GetDataMarkChannel().position / 4.0f;

//             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);// 参考下面// if (k < numNegativeBands)那行开始修改
//             originalPoints.Add(pointOrigin);
//         }

//         for (int i = 0; i < numLayers; i++)
//         {
//             List<Vector3> layerPoints = new List<Vector3>();
//             for (int j = 0; j < layeredValuesArray.Length; j++)
//             {
//                 if (layeredValuesArray[j] == i)
//                 {
//                     layerPoints.Add(originalPoints[j]);
//                 }
//             }
//             List<Vector3> layerPointsTemp = new List<Vector3>(layerPoints);
//             if (layerPointsTemp.Count > 0)
//             {
//                 float bottomLimit = layerPointsTemp[0].y;
//                 for (int j = layerPointsTemp.Count - 2; j >= 0; j--)
//                 {
//                     layerPoints.Add(new Vector3(layerPointsTemp[j].x, bottomLimit, layerPointsTemp[j].z));
//                 }
//                 restructuredData[i] = layerPoints;
//             }
//             else
//             {
//                 restructuredData[i] = layerPoints;
//             }
//         }

//         return restructuredData;
//     }

//     private void ConnectLayerPoints(List<Vector3>[] dataList)
//     {
//         foreach (var layer in dataList)
//         {
//             if (layer.Count != 0)
//             {
//                 ConnectLayerPoints(layer);
//             }
//         }
//     }

//     private void ConnectLayerPoints(List<Vector3> dataList)
//     {
//         GameObject lineObject = new GameObject("LineRenderer");
//         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
//         // lineRenderer.transform.SetParent(container.transform);
        
//         lineRenderer.positionCount = dataList.Count;
//         lineRenderer.startWidth = 0.0002f;
//         lineRenderer.endWidth = 0.0002f;
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(dataList.ToArray());
//     }
    
//     private void CreateFillArea(List<Vector3>[] dataList, int numNegativeBands, bool flagCompress, bool flagFlips)
//     {
//         Color darkRed = new Color(139/255f, 0f, 0f, 65/240f);
//         Color lightRed = new Color(255/255f, 128/255f, 128/255f, 220/240f);
//         Color darkBlue = new Color(0f, 0f, 139/255f, 65/240f);
//         Color lightBlue = new Color(153/255f, 217/255f, 234/255f, 182/240f);
//         Color[] selectedColorsPositive = new Color[] {lightRed, darkRed};
//         Color[] selectedColorsNegative = new Color[] {darkBlue, lightBlue};
//         Color[] selectedColorsNegativeReversed = (Color[])selectedColorsNegative.Clone();
//         Array.Reverse(selectedColorsNegativeReversed);
//         double maxV = double.NegativeInfinity;
//         double minV = double.PositiveInfinity;
//         double baseLinePosition = 0.0;
//         double modifiedHeightBand;
//         if (flagCompress && dataList.Length > 2)
//         {
//             // Debug.Log(dataList.Length);
//             int targetIndex = (int)Math.Ceiling(dataList.Length / 2.0);
//             // Debug.Log(targetIndex);
//             modifiedHeightBand = dataList[targetIndex + 1][0].y - dataList[targetIndex][0].y;
//             // Debug.Log(dataList[targetIndex][0].y);
//             // Debug.Log(modifiedHeightBand);
//             // modifiedHeightBand = dataList[1][0].y - dataList[0][0].y;
//         }
//         else
//         {
//             modifiedHeightBand = 0.0;
//         }
//         float pointX;
//         float pointY;
//         float pointZ;
//         float zDepth = 0.00001f;
        
//         for (int listIndex = 0; listIndex < dataList.Length; listIndex++)
//         {
//             foreach (var V in dataList[listIndex])
//             {
//                 if (V.y * 4.0f > maxV)
//                 {
//                     maxV = V.y * 4.0f;
//                 }
//                 if (V.y * 4.0f < minV)
//                 {
//                     minV = V.y * 4.0f;
//                 }
//             }
//             if (listIndex == numNegativeBands)
//             {
//                 baseLinePosition = dataList[listIndex][0].y;
//             }
//         }
//         double modifiedBaseLine = dataList[numNegativeBands][0].y * 4.0f;

//         for (int k = 0; k < dataList.Length; k++)
//         {
//             if (dataList[k].Count != 0)
//             {
//                 Vector3[] points = new Vector3[dataList[k].Count];
//                 double maxLayerV = double.NegativeInfinity;
//                 double minLayerV = double.PositiveInfinity;
//                 if (k < numNegativeBands && !flagFlips)
//                 {
//                     for (int kk = 0; kk < dataList[k].Count; kk++)
//                     {
//                         pointX = dataList[k][kk].x * 4.0f;
//                         pointY = (float)(dataList[k][kk].y + modifiedHeightBand * (numNegativeBands - k - 1)) * 4.0f;
//                         pointZ = dataList[k][kk].z * 4.0f + zDepth * k;
//                         points[kk] = new Vector3(pointX, pointY, pointZ);
//                         if (maxLayerV < dataList[k][kk].y * 4.0f)
//                         {
//                             maxLayerV = dataList[k][kk].y * 4.0f;
//                         }
//                         if (minLayerV > dataList[k][kk].y * 4.0f)
//                         {
//                             minLayerV = dataList[k][kk].y * 4.0f;
//                         }
//                     }
//                 }
//                 else if (k < numNegativeBands && flagFlips)
//                 {
//                     for (int kk = 0; kk < dataList[k].Count; kk++)
//                     {
//                         pointX = dataList[k][kk].x * 4.0f;
//                         pointY = 2.0f * (float)modifiedBaseLine - (float)(dataList[k][kk].y + modifiedHeightBand * (numNegativeBands - k - 1)) * 4.0f;
//                         pointZ = dataList[k][kk].z * 4.0f + zDepth * k;
//                         points[kk] = new Vector3(pointX, pointY, pointZ);
//                         if (maxLayerV < dataList[k][kk].y * 4.0f)
//                         {
//                             maxLayerV = dataList[k][kk].y * 4.0f;
//                         }
//                         if (minLayerV > dataList[k][kk].y * 4.0f)
//                         {
//                             minLayerV = dataList[k][kk].y * 4.0f;
//                         }
//                     }
//                 }
//                 else
//                 {
//                     for (int kk = 0; kk < dataList[k].Count; kk++)
//                     {
//                         pointX = dataList[k][kk].x * 4.0f;
//                         pointY = (float)(dataList[k][kk].y - modifiedHeightBand * (k - numNegativeBands)) * 4.0f;
//                         pointZ = dataList[k][kk].z * 4.0f - zDepth * k;
//                         points[kk] = new Vector3(pointX, pointY, pointZ);
//                         if (maxLayerV < dataList[k][kk].y * 4.0f)
//                         {
//                             maxLayerV = dataList[k][kk].y * 4.0f;
//                         }
//                         if (minLayerV > dataList[k][kk].y * 4.0f)
//                         {
//                             minLayerV = dataList[k][kk].y * 4.0f;
//                         }
//                     }
//                 }

//                 GameObject fillAreaObject = new GameObject("FillArea");
//                 fillAreaObject.transform.SetParent(visContainerObject.transform);
//                 MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
//                 MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
//                 Mesh mesh = new Mesh();
//                 mesh.vertices = points;
//                 int[] triangles = new int[(points.Length - 1) * 3];
//                 int index = 0;
//                 if (k < numNegativeBands && !flagFlips)
//                 {
//                     for (int i = 0; i < points.Length - 1; i++)
//                     {
//                         triangles[index++] = points.Length - i - 1;
//                         triangles[index++] = i + 1;
//                         triangles[index++] = i;
//                     }
//                     meshRenderer.material = new Material(Shader.Find("Standard"));
//                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, minV, modifiedBaseLine, selectedColorsNegative);
//                 }
//                 else if (k < numNegativeBands && flagFlips)
//                 {
//                     for (int i = 0; i < points.Length - 1; i++)
//                     {
//                         triangles[index++] = i;
//                         triangles[index++] = i + 1;
//                         triangles[index++] = points.Length - i - 1;
//                     }
//                     meshRenderer.material = new Material(Shader.Find("Standard"));
//                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, minV, modifiedBaseLine, selectedColorsNegative);
//                 }
//                 else
//                 {
//                     for (int i = 0; i < points.Length - 1; i++)
//                     {
//                         triangles[index++] = i;
//                         triangles[index++] = i + 1;
//                         triangles[index++] = points.Length - i - 1;
//                     }
//                     meshRenderer.material = new Material(Shader.Find("Standard"));
//                     meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2.0f, modifiedBaseLine, maxV, selectedColorsPositive);
//                 }
//                 mesh.triangles = triangles;
//                 mesh.RecalculateNormals();
//                 // Debug.Log(mesh.normals[3]);
//                 meshFilter.mesh = mesh;
//             }
//         }
//     }

//     private void ConnectPoints(List<DataMark> dataList)
//     {
//         GameObject lineObject = new GameObject("LineRenderer");
//         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        
//         List<Vector3> points = new List<Vector3>();

//         foreach (var point in dataList)
//         {
//             var p = point.GetDataMarkChannel().position / 4.0f;
//             points.Add(p);
//         }

//         lineRenderer.positionCount = points.Count;
//         lineRenderer.startWidth = 0.0002f;
//         lineRenderer.endWidth = 0.0002f;
//         // lineRenderer.startWidth = 0.000f;
//         // lineRenderer.endWidth = 0.000f;
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(points.ToArray());
//     }
// }