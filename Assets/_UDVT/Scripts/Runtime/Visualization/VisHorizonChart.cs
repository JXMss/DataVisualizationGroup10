// using System.Linq;
// using System;
// using UnityEngine;
// using System.Collections.Generic;

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

//         // double bandHeight = 10.0f;
//         double bandHeight = 0.05f;
//         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
//         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
//         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();
//         // Debug.Log(xValues.Length);
//         // for (int i = 0; i < xValues.Length; i++)
//         // {
//         //     Debug.Log(xValues[i]);
//         // }
//         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight);
//         double[] xValuesProcessed = xyValue.xValues;
//         Debug.Log(xValuesProcessed.Length);
//         double[] yValuesProcessed = xyValue.yValues;
//         // Debug.Log(yValuesProcessed.Length);

//         //## 01:  Create Axes and Grids
//         // X Axis
//         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
//         visContainer.CreateGrid(Direction.X, Direction.Y);

//         // Y Axis
//         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
//         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

//         //## 02: Set Remaining Vis Channels (Color,...)
//         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
//         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
//         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
//         double[] sizeList = new double[xValuesProcessed.Length];
//         // for (int i = 0; i < sizeList.Length; i++)
//         // {
//         //     sizeList[i] = 0.0001;
//         // }

//         visContainer.SetChannel(VisChannel.XSize, sizeList);
//         visContainer.SetChannel(VisChannel.YSize, sizeList);
//         visContainer.SetChannel(VisChannel.ZSize, sizeList);
//         //## 03: Draw all Data Points with the provided Channels 
//         visContainer.CreateDataMarks(dataMarkPrefab);
//         // foreach (var i in visContainer.dataAxisList)
//         // {
//         //     Debug.Log(visContainer.GetAxisScale(Direction.X));
//         // }
//         Draw(visContainer.dataMarkList, bandHeight);
//         // ConnectPoints(visContainer.dataMarkList);
//         //## 04: Rescale Chart
//         visContainerObject.transform.localScale = new Vector3(width, height, depth);
//         // CreateFillArea(visContainer.dataMarkList, blue);

//         return visContainerObject;
//     }
    
//     public class xyValues
//     {
//         public double[] xValues { get; set; }
//         public double[] yValues { get; set; }
//     }

//     private bool IsInTheBand(double point, double upLimit, double bottomLimit)
//     {
//         return point < upLimit && point >= bottomLimit;
//     }
    
//     private double GetClosestBandLineLower(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return (numBandLine - 1) * bandHeight;
//     }

//     private double GetClosestBandLineHigher(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return numBandLine * bandHeight;
//     }

//     private xyValues GetJunctionPoint(double[] dataListX, double[] dataListY, double bandHeight)
//     {
//         double maxY = 0.0;
//         double minY = 0.0;
//         double previousPointX = 0.0;
//         double previousPointY = 0.0;
//         int numJunctionPoints = 0;

//         // Debug.Log(dataListX.Length);
//         // Debug.Log(dataListY.Length);
//         for (int i = 0; i < dataListX.Length; i++)
//         {
//             // Debug.Log("=================================================");
//             // Debug.Log(dataListY[i]);
//             double Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             // Debug.Log(Lower);
//             double Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
//             // Debug.Log(Higher);
//             // Debug.Log("=================================================");
//             // Debug.Log(IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower));
//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 // Debug.Log("======================" + i + "===========================");
//                 // Debug.Log("previousPointY                       " + previousPointY);
//                 // Debug.Log("dataListY[i]                         " + dataListY[i]);
//                 // Debug.Log("Lower                                " + Lower);
//                 // Debug.Log("Higher                               " + Higher);
//                 // Debug.Log(IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower));
//                 numJunctionPoints += 1;
//             }
//             // Debug.Log(numJunctionPoints);
//             previousPointY = dataListY[i];
//             // Debug.Log(previousPointY);
//         }
//         // Debug.Log("///////////////////////////////////////////////");
//         // Debug.Log(numJunctionPoints);
        
//         previousPointX = dataListX[0];
//         previousPointY = dataListY[0];
//         double[] xValueModified = new double[dataListX.Length + numJunctionPoints];
//         double[] yValueModified = new double[dataListX.Length + numJunctionPoints];
//         int[] junctionPoints = new int[numJunctionPoints];
//         int numjunctionPointsindex = 0;


//         xValueModified[0] = dataListX[0];
//         yValueModified[0] = dataListY[0];
//         for (int i = 1; i < dataListX.Length; i++)
//         {
//             double Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             double Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
//             // xValueModified[i + numjunctionPointsindex] = dataListX[i];
//             // yValueModified[i + numjunctionPointsindex] = dataListY[i];
//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 // Debug.Log("======================" + i + "===========================");
//                 // Debug.Log("previousPointY                       " + previousPointY);
//                 // Debug.Log("dataListY[i]                         " + dataListY[i]);
//                 // Debug.Log("Lower                                " + Lower);
//                 // Debug.Log("Higher                               " + Higher);
//                 // Debug.Log("xPercentage                          " + Higher);
//                 // Debug.Log("x                                    " + Higher);
//                 // Debug.Log("y                                    " + Higher);
//                 // numjunctionPointsindex += 1;
//                 // double closestLine = GetClosestBandLineLower(dataListY[i], bandHeight);
                
//                 if (dataListY[i] > previousPointY)
//                 {
//                     double xPercentage = (dataListY[i] - Lower) / (dataListY[i] - previousPointY);
//                     double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
//                     xValueModified[i + numjunctionPointsindex] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex] = Lower;
//                     numjunctionPointsindex += 1;
//                     xValueModified[i + numjunctionPointsindex] = dataListX[i];
//                     yValueModified[i + numjunctionPointsindex] = dataListY[i];
//                 }
//                 else if (dataListY[i] < previousPointY)
//                 {
//                     double xPercentage = (previousPointY - Higher) / (previousPointY - dataListY[i]);
//                     double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
//                     xValueModified[i + numjunctionPointsindex] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex] = Higher;
//                     numjunctionPointsindex += 1;
//                     xValueModified[i + numjunctionPointsindex] = dataListX[i];
//                     yValueModified[i + numjunctionPointsindex] = dataListY[i];
//                 }
//             }
//             else
//             {
//                 xValueModified[i + numjunctionPointsindex] = dataListX[i];
//                 yValueModified[i + numjunctionPointsindex] = dataListY[i];
//             }
//             // Debug.Log("======================" + i + "===========================");
//             // Debug.Log("previousPointX                       " + previousPointX);
//             // Debug.Log("dataListY[i]                         " + dataListY[i]);
//             // Debug.Log("Lower                                " + Lower);
//             // Debug.Log("Higher                               " + Higher);
//             // Debug.Log("xPercentage                          " + Higher);
//             // Debug.Log("x                                    " + Higher);
//             // Debug.Log("y                                    " + Higher);
//             previousPointX = dataListX[i];
//             previousPointY = dataListY[i];
//         }
        
//         return new xyValues { xValues = xValueModified, yValues = yValueModified };
//     }

//     private void Draw(List<DataMark> dataList, double bandHeight)
//     {
//         List<List<Vector3>> layeredData = new List<List<Vector3>>();
//         layeredData = LayeringData(dataList, bandHeight);
//         ConnectLayerPoints(layeredData);
//     }

//     private List<List<Vector3>> LayeringData(List<DataMark> dataList, double bandHeight)
//     {
//         List<List<Vector3>> layers = new List<List<Vector3>>();
//         List<Vector3> originalPoints = new List<Vector3>();
//         double maxY = 0;

//         foreach (var point in dataList)
//         {
//             var p = point.GetDataMarkChannel().position / 4.0f;
//             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
//             originalPoints.Add(pointOrigin);
//             if (maxY < pointOrigin.y)
//             {
//                 maxY = pointOrigin.y;
//             }
//         }

//         int numLayers = (int)Math.Ceiling(maxY / bandHeight);

//         for (int i = 0; i < numLayers; i++)
//         {
//             List<Vector3> layerPoints = new List<Vector3>();
//             foreach (var point in originalPoints)
//             {
//                 if (IsInTheBand(point.y, (i + 1) * bandHeight, i * bandHeight))
//                 {
//                     layerPoints.Add(point);
//                 }
//             }
//             layers.Add(layerPoints);
//         }

//         return layers;
//     }

//     private void ConnectLayerPoints(List<List<Vector3>> dataList)
//     {
//         foreach (var layer in dataList)
//         {
//             ConnectLayerPoints(layer);
//         }
//     }

//     private void ConnectLayerPoints(List<Vector3> dataList)
//     {
//         GameObject lineObject = new GameObject("LineRenderer");
//         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
//         // lineRenderer.transform.SetParent(container.transform);
        
//         lineRenderer.positionCount = dataList.Count;
//         lineRenderer.startWidth = 0.001f;
//         lineRenderer.endWidth = 0.001f;
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(dataList.ToArray());
//     }
    
//     private void CreateFillArea(List<DataMark> dataList, string selectedcolor)
//     {
//         Vector3[] points = dataList.Select(dataMark => dataMark.GetDataMarkChannel().position / 4.0f).ToArray();

//         GameObject fillAreaObject = new GameObject("FillArea");
//         fillAreaObject.transform.SetParent(visContainerObject.transform);
//         MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
//         MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
//         Mesh mesh = new Mesh();
//         mesh.vertices = points;
//         int[] triangles = new int[(points.Length - 1) * 3];
//         int index = 0;
//         for (int i = 0; i < points.Length - 1; i++)
//         {
//             triangles[index++] = i;
//             triangles[index++] = i + 1;
//             triangles[index++] = points.Length - i - 1;
//         }
//         mesh.triangles = triangles;
//         mesh.RecalculateNormals();
//         meshFilter.mesh = mesh;
//         meshRenderer.material = new Material(Shader.Find("Standard"));
//         switch(selectedcolor)
//         {
//             case "blue":
//                 meshRenderer.material.color = Color.blue;
//                 break;
//             case "red":
//                 meshRenderer.material.color = Color.red;
//                 break;
//             case "yellow":
//                 meshRenderer.material.color = Color.yellow;
//                 break;
//             case "green":
//                 meshRenderer.material.color = Color.green;
//                 break;
//             case "black":
//                 meshRenderer.material.color = Color.black;
//                 break;
//             default:
//                 meshRenderer.material.color = Color.white;
//                 break;
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
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(points.ToArray());
//     }
// }

// using System.Linq;
// using System;
// using UnityEngine;
// using System.Collections.Generic;

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

//         // double bandHeight = 10.0f;
//         double bandHeight = 0.01f;
//         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
//         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
//         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

//         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight);
//         double[] xValuesProcessed = xyValue.xValues;
//         // Debug.Log(xValuesProcessed.Length);
//         double[] yValuesProcessed = xyValue.yValues;
//         // Debug.Log(yValuesProcessed.Length);
//         int[] layeredValues = xyValue.layeredIndex;
//         //## 01:  Create Axes and Grids
//         // X Axis
//         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
//         visContainer.CreateGrid(Direction.X, Direction.Y);

//         // Y Axis
//         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
//         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

//         //## 02: Set Remaining Vis Channels (Color,...)
//         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
//         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
//         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
//         double[] sizeList = new double[xValuesProcessed.Length];
//         // for (int i = 0; i < sizeList.Length; i++)
//         // {
//         //     sizeList[i] = 0.0001;
//         // }

//         visContainer.SetChannel(VisChannel.XSize, sizeList);
//         visContainer.SetChannel(VisChannel.YSize, sizeList);
//         visContainer.SetChannel(VisChannel.ZSize, sizeList);
//         //## 03: Draw all Data Points with the provided Channels 
//         visContainer.CreateDataMarks(dataMarkPrefab);
//         // foreach (var i in visContainer.dataAxisList)
//         // {
//         //     Debug.Log(visContainer.GetAxisScale(Direction.X));
//         // }
//         Draw(visContainer.dataMarkList, bandHeight, layeredValues);
//         // ConnectPoints(visContainer.dataMarkList);
//         //## 04: Rescale Chart
//         visContainerObject.transform.localScale = new Vector3(width, height, depth);

//         return visContainerObject;
//     }
    
//     public class xyValues
//     {
//         public double[] xValues { get; set; }
//         public double[] yValues { get; set; }
//         public int[] layeredIndex { get; set; }
//     }

//     private bool IsInTheBand(double point, double upLimit, double downLimit)
//     {
//         return point <= upLimit && point >= downLimit;
//     }
    
//     private double GetClosestBandLineLower(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return (numBandLine - 1) * bandHeight;
//     }

//     private double GetClosestBandLineHigher(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return numBandLine * bandHeight;
//     }

//     private xyValues GetJunctionPoint(double[] dataListX, double[] dataListY, double bandHeight)
//     {
//         double maxY = 0.0;
//         double minY = 0.0;
//         double previousPointX = 0.0;
//         double previousPointY = 0.0;
//         double Lower;
//         double Higher;
//         int numJunctionPoints = 0;

//         for (int i = 0; i < dataListX.Length; i++)
//         {
//             Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
            
//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 numJunctionPoints += 2;
//             }
//             previousPointY = dataListY[i];
//         }
        
//         previousPointX = dataListX[0];
//         previousPointY = dataListY[0];
//         double[] xValueModified = new double[dataListX.Length + numJunctionPoints + 2];
//         double[] yValueModified = new double[dataListX.Length + numJunctionPoints + 2];
//         int[] layeredDataArray = new int[dataListX.Length + numJunctionPoints + 2];

//         int numjunctionPointsindex = 0;

        
//         double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
//         xValueModified[0] = dataListX[0];
//         yValueModified[0] = Lower0;
//         layeredDataArray[0] = (int)(Lower0 / bandHeight);
//         xValueModified[1] = dataListX[0];
//         yValueModified[1] = dataListY[0];
//         layeredDataArray[1] = (int)(Lower0 / bandHeight);

//         for (int i = 1; i < dataListX.Length; i++)
//         {
//             Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);

//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 if (dataListY[i] > previousPointY)
//                 {
//                     double xPercentage = (dataListY[i] - Lower) / (dataListY[i] - previousPointY);
//                     double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Lower;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight) - 1;
//                     numjunctionPointsindex += 1;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Lower;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Higher / bandHeight) - 1;
//                 }
//                 else if (dataListY[i] < previousPointY)
//                 {
//                     double xPercentage = (previousPointY - Higher) / (previousPointY - dataListY[i]);
//                     double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Higher;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//                     numjunctionPointsindex += 1;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Higher;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Higher / bandHeight);
//                 }
//                 numjunctionPointsindex += 1;
//                 xValueModified[i + numjunctionPointsindex + 1] = dataListX[i];
//                 yValueModified[i + numjunctionPointsindex + 1] = dataListY[i];
//                 layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//             }
//             else
//             {
//                 xValueModified[i + numjunctionPointsindex + 1] = dataListX[i];
//                 yValueModified[i + numjunctionPointsindex + 1] = dataListY[i];
//                 layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//             }
//             previousPointX = dataListX[i];
//             previousPointY = dataListY[i];
//         }
//         Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
//         xValueModified[dataListX.Length + numJunctionPoints + 1] = dataListX[dataListX.Length - 1];
//         yValueModified[dataListX.Length + numJunctionPoints + 1] = Lower;
//         layeredDataArray[dataListX.Length + numJunctionPoints + 1] = (int)(Lower / bandHeight) - 1;
//         return new xyValues { xValues = xValueModified, yValues = yValueModified, layeredIndex = layeredDataArray };
//     }

//     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray)
//     {
//         int maxNumLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
//         layeredData = LayeringData(dataList, layeredValuesArray);
//         ConnectLayerPoints(layeredData);
//         CreateFillArea(layeredData, "blue");
//     }

//     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
//     {
//         List<List<Vector3>> layers = new List<List<Vector3>>();
//         List<Vector3> originalPoints = new List<Vector3>();
//         double maxY = 0;
//         int numLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

//         foreach (var point in dataList)
//         {
//             var p = point.GetDataMarkChannel().position / 4.0f;
//             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
//             originalPoints.Add(pointOrigin);
//             if (maxY < pointOrigin.y)
//             {
//                 maxY = pointOrigin.y;
//             }
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
//         }

//         return restructuredData;
//     }

//     private void ConnectLayerPoints(List<Vector3>[] dataList)
//     {
//         foreach (var layer in dataList)
//         {
//             ConnectLayerPoints(layer);
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
    
//     private void CreateFillArea(List<Vector3>[] dataList, string selectedcolor)
//     {
//         string[] selectedcolors = new string[] {"blue", "red", "yellow", "green", "black"};
//         for (int k = 0; k < dataList.Length; k++)
//         {
//             Vector3[] points = new Vector3[dataList[k].Count];
//             for (int i = 0; i < dataList[k].Count; i++)
//             {
//                 points[i] = dataList[k][i] * 4.0f;
//             }
//             selectedcolor = selectedcolors[k % 5];

//             GameObject fillAreaObject = new GameObject("FillArea");
//             fillAreaObject.transform.SetParent(visContainerObject.transform);
//             MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
//             MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
//             Mesh mesh = new Mesh();
//             mesh.vertices = points;
//             int[] triangles = new int[(points.Length - 1) * 3];
//             int index = 0;
//             for (int i = 0; i < points.Length - 1; i++)
//             {
//                 triangles[index++] = i;
//                 triangles[index++] = i + 1;
//                 triangles[index++] = points.Length - i - 1;
//             }
//             mesh.triangles = triangles;
//             mesh.RecalculateNormals();
//             meshFilter.mesh = mesh;
//             meshRenderer.material = new Material(Shader.Find("Standard"));
//             switch(selectedcolor)
//             {
//                 case "blue":
//                     meshRenderer.material.color = Color.blue;
//                     break;
//                 case "red":
//                     meshRenderer.material.color = Color.red;
//                     break;
//                 case "yellow":
//                     meshRenderer.material.color = Color.yellow;
//                     break;
//                 case "green":
//                     meshRenderer.material.color = Color.green;
//                     break;
//                 case "black":
//                     meshRenderer.material.color = Color.black;
//                     break;
//                 default:
//                     meshRenderer.material.color = Color.white;
//                     break;
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
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(points.ToArray());
//     }
// }

// using System.Linq;
// using System;
// using UnityEngine;
// using System.Collections.Generic;

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

//         // double bandHeight = 10.0f;
//         double bandHeight = 0.005f;
//         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
//         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
//         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

//         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight);
//         double[] xValuesProcessed = xyValue.xValues;
//         // Debug.Log(xValuesProcessed.Length);
//         double[] yValuesProcessed = xyValue.yValues;
//         // Debug.Log(yValuesProcessed.Length);
//         int[] layeredValues = xyValue.layeredIndex;
//         //## 01:  Create Axes and Grids
//         // X Axis
//         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
//         visContainer.CreateGrid(Direction.X, Direction.Y);

//         // Y Axis
//         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
//         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

//         //## 02: Set Remaining Vis Channels (Color,...)
//         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
//         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
//         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
//         double[] sizeList = new double[xValuesProcessed.Length];
//         // for (int i = 0; i < sizeList.Length; i++)
//         // {
//         //     sizeList[i] = 0.0001;
//         // }

//         visContainer.SetChannel(VisChannel.XSize, sizeList);
//         visContainer.SetChannel(VisChannel.YSize, sizeList);
//         visContainer.SetChannel(VisChannel.ZSize, sizeList);
//         //## 03: Draw all Data Points with the provided Channels 
//         visContainer.CreateDataMarks(dataMarkPrefab);
//         // foreach (var i in visContainer.dataAxisList)
//         // {
//         //     Debug.Log(visContainer.GetAxisScale(Direction.X));
//         // }
//         Draw(visContainer.dataMarkList, bandHeight, layeredValues);
//         // ConnectPoints(visContainer.dataMarkList);
//         //## 04: Rescale Chart
//         visContainerObject.transform.localScale = new Vector3(width, height, depth);

//         return visContainerObject;
//     }
    
//     public class xyValues
//     {
//         public double[] xValues { get; set; }
//         public double[] yValues { get; set; }
//         public int[] layeredIndex { get; set; }
//     }

//     private bool IsInTheBand(double point, double upLimit, double downLimit)
//     {
//         return point <= upLimit && point >= downLimit;
//     }
    
//     private double GetClosestBandLineLower(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return (numBandLine - 1) * bandHeight;
//     }

//     private double GetClosestBandLineHigher(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return numBandLine * bandHeight;
//     }

//     private xyValues GetJunctionPoint(double[] dataListX, double[] dataListY, double bandHeight)
//     {
//         double maxY = 0.0;
//         double minY = 0.0;
//         double previousPointX = 0.0;
//         double previousPointY = 0.0;
//         double Lower;
//         double Higher;
//         int numJunctionPoints = 0;

//         for (int i = 0; i < dataListX.Length; i++)
//         {
//             Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
            
//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 numJunctionPoints += 2;
//             }
//             previousPointY = dataListY[i];
//         }
        
//         previousPointX = dataListX[0];
//         previousPointY = dataListY[0];
//         double[] xValueModified = new double[dataListX.Length + numJunctionPoints + 2];
//         double[] yValueModified = new double[dataListX.Length + numJunctionPoints + 2];
//         int[] layeredDataArray = new int[dataListX.Length + numJunctionPoints + 2];

//         int numjunctionPointsindex = 0;

        
//         double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
//         xValueModified[0] = dataListX[0];
//         yValueModified[0] = Lower0;
//         layeredDataArray[0] = (int)(Lower0 / bandHeight);
//         xValueModified[1] = dataListX[0];
//         yValueModified[1] = dataListY[0];
//         layeredDataArray[1] = (int)(Lower0 / bandHeight);

//         for (int i = 1; i < dataListX.Length; i++)
//         {
//             Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);

//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 if (dataListY[i] > previousPointY)
//                 {
//                     // for (int j = 1;)
//                     double xPercentage = (dataListY[i] - Lower) / (dataListY[i] - previousPointY);
//                     double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Lower;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight) - 1;
//                     numjunctionPointsindex += 1;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Lower;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Higher / bandHeight) - 1;
//                 }
//                 else if (dataListY[i] < previousPointY)
//                 {
//                     double xPercentage = (previousPointY - Higher) / (previousPointY - dataListY[i]);
//                     double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Higher;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//                     numjunctionPointsindex += 1;
//                     xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                     yValueModified[i + numjunctionPointsindex + 1] = Higher;
//                     layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Higher / bandHeight);
//                 }
//                 numjunctionPointsindex += 1;
//                 xValueModified[i + numjunctionPointsindex + 1] = dataListX[i];
//                 yValueModified[i + numjunctionPointsindex + 1] = dataListY[i];
//                 layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//             }
//             else
//             {
//                 xValueModified[i + numjunctionPointsindex + 1] = dataListX[i];
//                 yValueModified[i + numjunctionPointsindex + 1] = dataListY[i];
//                 layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//             }
//             previousPointX = dataListX[i];
//             previousPointY = dataListY[i];
//         }
//         Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
//         xValueModified[dataListX.Length + numJunctionPoints + 1] = dataListX[dataListX.Length - 1];
//         yValueModified[dataListX.Length + numJunctionPoints + 1] = Lower;
//         layeredDataArray[dataListX.Length + numJunctionPoints + 1] = (int)(Lower / bandHeight) - 1;
//         return new xyValues { xValues = xValueModified, yValues = yValueModified, layeredIndex = layeredDataArray };
//     }

//     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray)
//     {
//         int maxNumLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
//         layeredData = LayeringData(dataList, layeredValuesArray);
//         ConnectLayerPoints(layeredData);
//         CreateFillArea(layeredData, "blue");
//     }

//     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
//     {
//         List<List<Vector3>> layers = new List<List<Vector3>>();
//         List<Vector3> originalPoints = new List<Vector3>();
//         double maxY = 0;
//         int numLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

//         foreach (var point in dataList)
//         {
//             var p = point.GetDataMarkChannel().position / 4.0f;
//             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
//             originalPoints.Add(pointOrigin);
//             if (maxY < pointOrigin.y)
//             {
//                 maxY = pointOrigin.y;
//             }
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
//         }

//         return restructuredData;
//     }

//     private void ConnectLayerPoints(List<Vector3>[] dataList)
//     {
//         foreach (var layer in dataList)
//         {
//             ConnectLayerPoints(layer);
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
    
//     private void CreateFillArea(List<Vector3>[] dataList, string selectedcolor)
//     {
//         string[] selectedcolors = new string[] {"blue", "red", "yellow", "green", "black"};
//         for (int k = 0; k < dataList.Length; k++)
//         {
//             Vector3[] points = new Vector3[dataList[k].Count];
//             for (int i = 0; i < dataList[k].Count; i++)
//             {
//                 points[i] = dataList[k][i] * 4.0f;
//             }
//             selectedcolor = selectedcolors[k % 5];

//             GameObject fillAreaObject = new GameObject("FillArea");
//             fillAreaObject.transform.SetParent(visContainerObject.transform);
//             MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
//             MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
//             Mesh mesh = new Mesh();
//             mesh.vertices = points;
//             int[] triangles = new int[(points.Length - 1) * 3];
//             int index = 0;
//             for (int i = 0; i < points.Length - 1; i++)
//             {
//                 triangles[index++] = i;
//                 triangles[index++] = i + 1;
//                 triangles[index++] = points.Length - i - 1;
//             }
//             mesh.triangles = triangles;
//             mesh.RecalculateNormals();
//             meshFilter.mesh = mesh;
//             meshRenderer.material = new Material(Shader.Find("Standard"));
//             switch(selectedcolor)
//             {
//                 case "blue":
//                     meshRenderer.material.color = Color.blue;
//                     break;
//                 case "red":
//                     meshRenderer.material.color = Color.red;
//                     break;
//                 case "yellow":
//                     meshRenderer.material.color = Color.yellow;
//                     break;
//                 case "green":
//                     meshRenderer.material.color = Color.green;
//                     break;
//                 case "black":
//                     meshRenderer.material.color = Color.black;
//                     break;
//                 default:
//                     meshRenderer.material.color = Color.white;
//                     break;
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
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(points.ToArray());
//     }
// }

// using System.Linq;
// using System;
// using UnityEngine;
// using System.Collections.Generic;

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

//         // double bandHeight = 10.0f;
//         double bandHeight = 0.005f;
//         // double bandHeight = 0.01f;
//         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
//         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
//         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

//         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight);
//         double[] xValuesProcessed = xyValue.xValues;
//         // Debug.Log(xValuesProcessed.Length);
//         double[] yValuesProcessed = xyValue.yValues;
//         // Debug.Log(yValuesProcessed.Length);
//         int[] layeredValues = xyValue.layeredIndex;
//         for (int k = 0; k < xValuesProcessed.Length; k++)
//         {
//             if (yValuesProcessed[k] / bandHeight != layeredValues[k] + 1)
//             {
//                 Debug.Log("===============" + k + "===============");
//                 Debug.Log("x    " + xValuesProcessed[k]);
//                 Debug.Log("y    " + yValuesProcessed[k]);
//                 Debug.Log("H    " + bandHeight);
//                 Debug.Log("L    " + layeredValues[k]);
//                 Debug.Log("R    " + (yValuesProcessed[k] / bandHeight - 1));
//             }
//         }
//         //## 01:  Create Axes and Grids
//         // X Axis
//         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
//         visContainer.CreateGrid(Direction.X, Direction.Y);

//         // Y Axis
//         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
//         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

//         //## 02: Set Remaining Vis Channels (Color,...)
//         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
//         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
//         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
//         double[] sizeList = new double[xValuesProcessed.Length];
//         for (int i = 0; i < sizeList.Length; i++)
//         {
//             sizeList[i] = 0.01;
//         }

//         visContainer.SetChannel(VisChannel.XSize, sizeList);
//         visContainer.SetChannel(VisChannel.YSize, sizeList);
//         visContainer.SetChannel(VisChannel.ZSize, sizeList);
//         //## 03: Draw all Data Points with the provided Channels 
//         visContainer.CreateDataMarks(dataMarkPrefab);
//         // foreach (var i in visContainer.dataAxisList)
//         // {
//         //     Debug.Log(visContainer.GetAxisScale(Direction.X));
//         // }
//         Draw(visContainer.dataMarkList, bandHeight, layeredValues);
//         // ConnectPoints(visContainer.dataMarkList);
//         //## 04: Rescale Chart
//         visContainerObject.transform.localScale = new Vector3(width, height, depth);

//         return visContainerObject;
//     }
    
//     public class xyValues
//     {
//         public double[] xValues { get; set; }
//         public double[] yValues { get; set; }
//         public int[] layeredIndex { get; set; }
//     }

//     private bool IsInTheBand(double point, double upLimit, double downLimit)
//     {
//         return point <= upLimit && point >= downLimit;
//     }
    
//     private double GetClosestBandLineLower(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return (numBandLine - 1) * bandHeight;
//     }

//     private double GetClosestBandLineHigher(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight < yV)
//         {
//             numBandLine += 1;
//         }

//         return numBandLine * bandHeight;
//     }

//     private xyValues GetJunctionPoint(double[] dataListX, double[] dataListY, double bandHeight)
//     {
//         double maxY = 0.0;
//         double minY = 0.0;
//         double previousPointX = 0.0;
//         double previousPointY = 0.0;
//         double Lower;
//         double Higher;
//         int numJunctionPoints = 0;

//         for (int i = 0; i < dataListX.Length; i++)
//         {
//             Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
            
//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 int timeAddJunctionPoint = 1 + Math.Abs((int)((Lower - previousPointY) / bandHeight));
//                 // Debug.Log("=================================================");
//                 // Debug.Log(timeAddJunctionPoint);
//                 // Debug.Log(Lower);
//                 // Debug.Log(previousPointY);
//                 // Debug.Log(bandHeight);
//                 // Debug.Log(Lower - previousPointY);
//                 // Debug.Log(1 + (int)((Lower - previousPointY) / bandHeight));
//                 numJunctionPoints += 2 * timeAddJunctionPoint;
//             }
//             previousPointY = dataListY[i];
//         }
        
//         previousPointX = dataListX[0];
//         previousPointY = dataListY[0];
//         double[] xValueModified = new double[dataListX.Length + numJunctionPoints + 2];
//         double[] yValueModified = new double[dataListX.Length + numJunctionPoints + 2];
//         int[] layeredDataArray = new int[dataListX.Length + numJunctionPoints + 2];

//         int numjunctionPointsindex = 0;

        
//         double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
//         xValueModified[0] = dataListX[0];
//         yValueModified[0] = Lower0;
//         layeredDataArray[0] = (int)(Lower0 / bandHeight);
//         xValueModified[1] = dataListX[0];
//         yValueModified[1] = dataListY[0];
//         layeredDataArray[1] = (int)(Lower0 / bandHeight);

//         for (int i = 1; i < dataListX.Length; i++)
//         {
//             Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);

//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 if (dataListY[i] > previousPointY)
//                 {
//                     int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
//                     for (int j = timeAddJunctionPoint; j > 0; j--)
//                     {
//                         double tempLower = Lower - (j - 1) * bandHeight;
//                         double tempHigher = Higher - (j - 1) * bandHeight;
//                         double xPercentage = (dataListY[i] - tempLower + (j - 1) * bandHeight) / (dataListY[i] - previousPointY);
//                         double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
//                         xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                         yValueModified[i + numjunctionPointsindex + 1] = tempLower;
//                         layeredDataArray[i + numjunctionPointsindex + 1] = (int)(tempLower / bandHeight) - j;
//                         numjunctionPointsindex += 1;
//                         xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                         yValueModified[i + numjunctionPointsindex + 1] = tempLower;
//                         layeredDataArray[i + numjunctionPointsindex + 1] = (int)(tempHigher / bandHeight) - j;
//                         numjunctionPointsindex += 1;
//                     }
//                 }
//                 else if (dataListY[i] < previousPointY)
//                 {
//                     int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
//                     for (int j = timeAddJunctionPoint; j > 0; j--)
//                     {
//                         double tempLower = Lower + (j - 1) * bandHeight;
//                         double tempHigher = Higher + (j - 1) * bandHeight;
//                         double xPercentage = (previousPointY - tempHigher - (j - 1) * bandHeight) / (previousPointY - dataListY[i]);
//                         double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
//                         xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                         yValueModified[i + numjunctionPointsindex + 1] = tempHigher;
//                         layeredDataArray[i + numjunctionPointsindex + 1] = (int)(tempLower / bandHeight) + j - 1;
//                         numjunctionPointsindex += 1;
//                         xValueModified[i + numjunctionPointsindex + 1] = xJunctionPoint;
//                         yValueModified[i + numjunctionPointsindex + 1] = tempHigher;
//                         layeredDataArray[i + numjunctionPointsindex + 1] = (int)(tempHigher / bandHeight) + j - 1;
//                         numjunctionPointsindex += 1;
//                     }
//                 }
//                 xValueModified[i + numjunctionPointsindex + 1] = dataListX[i];
//                 yValueModified[i + numjunctionPointsindex + 1] = dataListY[i];
//                 layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//             }
//             else
//             {
//                 xValueModified[i + numjunctionPointsindex + 1] = dataListX[i];
//                 yValueModified[i + numjunctionPointsindex + 1] = dataListY[i];
//                 layeredDataArray[i + numjunctionPointsindex + 1] = (int)(Lower / bandHeight);
//             }
//             previousPointX = dataListX[i];
//             previousPointY = dataListY[i];
//         }
//         Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
//         xValueModified[dataListX.Length + numJunctionPoints + 1] = dataListX[dataListX.Length - 1];
//         yValueModified[dataListX.Length + numJunctionPoints + 1] = Lower;
//         layeredDataArray[dataListX.Length + numJunctionPoints + 1] = (int)(Lower / bandHeight) - 1;
//         return new xyValues { xValues = xValueModified, yValues = yValueModified, layeredIndex = layeredDataArray };
//     }

//     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray)
//     {
//         int maxNumLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
//         layeredData = LayeringData(dataList, layeredValuesArray);
//         ConnectLayerPoints(layeredData);
//         CreateFillArea(layeredData, "blue");
//     }

//     private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
//     {
//         List<List<Vector3>> layers = new List<List<Vector3>>();
//         List<Vector3> originalPoints = new List<Vector3>();
//         double maxY = 0;
//         int numLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

//         foreach (var point in dataList)
//         {
//             var p = point.GetDataMarkChannel().position / 4.0f;
//             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
//             originalPoints.Add(pointOrigin);
//             if (maxY < pointOrigin.y)
//             {
//                 maxY = pointOrigin.y;
//             }
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
//         }

//         return restructuredData;
//     }

//     private void ConnectLayerPoints(List<Vector3>[] dataList)
//     {
//         foreach (var layer in dataList)
//         {
//             ConnectLayerPoints(layer);
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
    
//     private void CreateFillArea(List<Vector3>[] dataList, string selectedcolor)
//     {
//         string[] selectedcolors = new string[] {"blue", "red", "yellow", "green", "black"};
//         for (int k = 0; k < dataList.Length; k++)
//         {
//             Vector3[] points = new Vector3[dataList[k].Count];
//             for (int i = 0; i < dataList[k].Count; i++)
//             {
//                 points[i] = dataList[k][i] * 4.0f;
//             }
//             selectedcolor = selectedcolors[k % 5];

//             GameObject fillAreaObject = new GameObject("FillArea");
//             fillAreaObject.transform.SetParent(visContainerObject.transform);
//             MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
//             MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
//             Mesh mesh = new Mesh();
//             mesh.vertices = points;
//             int[] triangles = new int[(points.Length - 1) * 3];
//             int index = 0;
//             for (int i = 0; i < points.Length - 1; i++)
//             {
//                 triangles[index++] = i;
//                 triangles[index++] = i + 1;
//                 triangles[index++] = points.Length - i - 1;
//             }
//             mesh.triangles = triangles;
//             mesh.RecalculateNormals();
//             meshFilter.mesh = mesh;
//             meshRenderer.material = new Material(Shader.Find("Standard"));
//             switch(selectedcolor)
//             {
//                 case "blue":
//                     meshRenderer.material.color = Color.blue;
//                     break;
//                 case "red":
//                     meshRenderer.material.color = Color.red;
//                     break;
//                 case "yellow":
//                     meshRenderer.material.color = Color.yellow;
//                     break;
//                 case "green":
//                     meshRenderer.material.color = Color.green;
//                     break;
//                 case "black":
//                     meshRenderer.material.color = Color.black;
//                     break;
//                 default:
//                     meshRenderer.material.color = Color.white;
//                     break;
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
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(points.ToArray());
//     }
// }


// using System.Linq;
// using System;
// using UnityEngine;
// using System.Collections.Generic;

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

//         // double bandHeight = 10.0f;
//         double bandHeight = 0.001f;
//         // double bandHeight = 0.01f;
//         double baseline = 0.0;
//         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
//         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
//         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

//         xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight, baseline);
//         double[] xValuesProcessed = xyValue.xValues;
//         // Debug.Log(xValuesProcessed.Length);
//         double[] yValuesProcessed = xyValue.yValues;
//         // Debug.Log(yValuesProcessed.Length);
//         int[] layeredValues = xyValue.layeredIndex;
//         //## 01:  Create Axes and Grids
//         // X Axis
//         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
//         visContainer.CreateGrid(Direction.X, Direction.Y);

//         // Y Axis
//         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
//         visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

//         //## 02: Set Remaining Vis Channels (Color,...)
//         visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
//         visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
//         visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
//         double[] sizeList = new double[xValuesProcessed.Length];
//         // for (int i = 0; i < sizeList.Length; i++)
//         // {
//         //     sizeList[i] = 0.01;
//         // }

//         visContainer.SetChannel(VisChannel.XSize, sizeList);
//         visContainer.SetChannel(VisChannel.YSize, sizeList);
//         visContainer.SetChannel(VisChannel.ZSize, sizeList);
//         //## 03: Draw all Data Points with the provided Channels 
//         visContainer.CreateDataMarks(dataMarkPrefab);
//         // foreach (var i in visContainer.dataAxisList)
//         // {
//         //     Debug.Log(visContainer.GetAxisScale(Direction.X));
//         // }
//         Draw(visContainer.dataMarkList, bandHeight, layeredValues);
//         // ConnectPoints(visContainer.dataMarkList);
//         //## 04: Rescale Chart
//         visContainerObject.transform.localScale = new Vector3(width, height, depth);

//         return visContainerObject;
//     }
    
//     public class xyValues
//     {
//         public double[] xValues { get; set; }
//         public double[] yValues { get; set; }
//         public int[] layeredIndex { get; set; }
//     }

//     private bool IsInTheBand(double point, double upLimit, double downLimit)
//     {
//         return point <= upLimit && point >= downLimit;
//     }
    
//     private double GetClosestBandLineLower(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight <= yV)
//         {
//             numBandLine += 1;
//         }

//         return (numBandLine - 1) * bandHeight;
//     }

//     private double GetClosestBandLineHigher(double yV, double bandHeight)
//     {
//         double closestBandLine = 0.0f;
//         int numBandLine = 1;
//         while (numBandLine * bandHeight <= yV)
//         {
//             numBandLine += 1;
//         }

//         return numBandLine * bandHeight;
//     }

//     private xyValues GetJunctionPoint(double[] oDataListX, double[] oDataListY, double bandHeight, double baseline)
//     {
//         double previousPointX;
//         double previousPointY;
//         double Lower;
//         double Higher;
//         double tempMaxY = 0.0;
//         double tempMinY = 0.0;
//         double[] dataListX = new double[oDataListX.Length + 2];
//         double[] dataListY = new double[oDataListY.Length + 2];

//         // Array.Copy(oDataListX, 0, dataListX, 1, oDataListX.Length);
//         // Array.Copy(oDataListY, 0, dataListY, 1, oDataListY.Length);

//         dataListX[0] = oDataListX[0];
//         dataListY[0] = 0.0 - baseline;

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

//         previousPointX = dataListX[0];
//         previousPointY = dataListY[0];
//         List<double> xValueModified = new List<double>();
//         List<double> yValueModified = new List<double>();
//         List<int> layeredDataArray = new List<int>();
        
//         double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
//         xValueModified.Add(dataListX[0]);
//         yValueModified.Add(Lower0);
//         layeredDataArray.Add((int)(Lower0 / bandHeight));
//         xValueModified.Add(dataListX[0]);
//         yValueModified.Add(dataListY[0]);
//         layeredDataArray.Add((int)(Lower0 / bandHeight));

//         for (int i = 1; i < dataListX.Length; i++)
//         {
//             Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
//             Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
//             // flagOnJunction = previ
//             if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
//             {
//                 if (dataListY[i] > previousPointY)
//                 {
//                     int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
//                     for (int j = timeAddJunctionPoint; j > 0; j--)
//                     {
//                         double tempLower = Lower - (j - 1) * bandHeight;
//                         double tempHigher = Higher - (j - 1) * bandHeight;
//                         double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
//                         double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
//                         xValueModified.Add(xJunctionPoint);
//                         yValueModified.Add(tempLower);
//                         layeredDataArray.Add((int)(tempLower / bandHeight) - 1);
//                         xValueModified.Add(xJunctionPoint);
//                         yValueModified.Add(tempLower);
//                         layeredDataArray.Add((int)(tempHigher / bandHeight) - 1);
//                     }
//                 }
//                 else if (dataListY[i] < previousPointY)
//                 {
//                     int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
//                     for (int j = timeAddJunctionPoint; j > 0; j--)
//                     {
//                         double tempLower = Lower + (j - 1) * bandHeight;
//                         double tempHigher = Higher + (j - 1) * bandHeight;
//                         double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
//                         double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
//                         xValueModified.Add(xJunctionPoint);
//                         yValueModified.Add(tempHigher);
//                         layeredDataArray.Add((int)(tempHigher / bandHeight));
//                         xValueModified.Add(xJunctionPoint);
//                         yValueModified.Add(tempHigher);
//                         layeredDataArray.Add((int)(tempLower / bandHeight));
//                     }
//                 }
//                 xValueModified.Add(dataListX[i]);
//                 yValueModified.Add(dataListY[i]);
//                 layeredDataArray.Add((int)(Lower / bandHeight));
//             }
//             else
//             {
//                 xValueModified.Add(dataListX[i]);
//                 yValueModified.Add(dataListY[i]);
//                 layeredDataArray.Add((int)(Lower / bandHeight));
//             }
//             previousPointX = dataListX[i];
//             previousPointY = dataListY[i];
//         }
//         Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
//         xValueModified.Add(dataListX[dataListX.Length - 1]);
//         yValueModified.Add(Lower);
//         layeredDataArray.Add((int)(Lower / bandHeight) - 1);
//         return new xyValues { xValues = xValueModified.ToArray(), yValues = yValueModified.ToArray(), layeredIndex = layeredDataArray.ToArray() };
//     }

//     private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray)
//     {
//         int maxNumLayers = layeredValuesArray.Max() + 1;
//         List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
//         layeredData = LayeringData(dataList, layeredValuesArray);
//         ConnectLayerPoints(layeredData);
//         CreateFillArea(layeredData);
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
//             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
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
    
//     private void CreateFillArea(List<Vector3>[] dataList)
//     {
//         string[] selectedcolors = new string[] {"blue", "red", "yellow", "green", "black"};
//         for (int k = 0; k < dataList.Length; k++)
//         {
//             if (dataList[k].Count != 0)
//             {
//                 Vector3[] points = new Vector3[dataList[k].Count];
//                 for (int i = 0; i < dataList[k].Count; i++)
//                 {
//                     points[i] = dataList[k][i] * 4.0f;
//                 }
//                 string selectedcolor = selectedcolors[k % 5];

//                 GameObject fillAreaObject = new GameObject("FillArea");
//                 fillAreaObject.transform.SetParent(visContainerObject.transform);
//                 MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
//                 MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
//                 Mesh mesh = new Mesh();
//                 mesh.vertices = points;
//                 int[] triangles = new int[(points.Length - 1) * 3];
//                 int index = 0;
//                 for (int i = 0; i < points.Length - 1; i++)
//                 {
//                     triangles[index++] = i;
//                     triangles[index++] = i + 1;
//                     triangles[index++] = points.Length - i - 1;
//                 }
//                 mesh.triangles = triangles;
//                 mesh.RecalculateNormals();
//                 meshFilter.mesh = mesh;
//                 meshRenderer.material = new Material(Shader.Find("Standard"));
//                 switch(selectedcolor)
//                 {
//                     case "blue":
//                         meshRenderer.material.color = Color.blue;
//                         break;
//                     case "red":
//                         meshRenderer.material.color = Color.red;
//                         break;
//                     case "yellow":
//                         meshRenderer.material.color = Color.yellow;
//                         break;
//                     case "green":
//                         meshRenderer.material.color = Color.green;
//                         break;
//                     case "black":
//                         meshRenderer.material.color = Color.black;
//                         break;
//                     default:
//                         meshRenderer.material.color = Color.white;
//                         break;
//                 }
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
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(points.ToArray());
//     }
// }


using System.Linq;
using System;
using UnityEngine;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.GraphView;

public class VisHorizonChart : Vis
{
    public VisHorizonChart()
    {
        title = "VisHorizon";
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }

    public override GameObject CreateVis(GameObject container)
    {
        base.CreateVis(container);

        // double bandHeight = 10.0f;
        double bandHeight = 0.001f;
        //double baseline = 0.006;
        double baseline = 0.0285;
        double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
        double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
        double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

        xyValues xyValue = GetJunctionPoint(xValues, yValues, bandHeight, baseline);
        double[] xValuesProcessed = xyValue.xValues;
        double[] yValuesProcessed = xyValue.yValues;
        int[] layeredValues = xyValue.layeredIndex;
        //## 01:  Create Axes and Grids
        // X Axis
        visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesProcessed, Direction.X);
        visContainer.CreateGrid(Direction.X, Direction.Y);

        // Y Axis
        //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
        visContainer.CreateAxis("Counter", yValuesProcessed, Direction.Y);

        //## 02: Set Remaining Vis Channels (Color,...)
        visContainer.SetChannel(VisChannel.XPos, xValuesProcessed);
        visContainer.SetChannel(VisChannel.YPos, yValuesProcessed);
        visContainer.SetChannel(VisChannel.Color, yValuesProcessed);
    
        double[] sizeList = new double[xValuesProcessed.Length];

        visContainer.SetChannel(VisChannel.XSize, sizeList);
        visContainer.SetChannel(VisChannel.YSize, sizeList);
        visContainer.SetChannel(VisChannel.ZSize, sizeList);
        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab);
        Draw(visContainer.dataMarkList, bandHeight, layeredValues);
        // ConnectPoints(visContainer.dataMarkList);
        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);

        return visContainerObject;
    }
    
    public class xyValues
    {
        public double[] xValues { get; set; }
        public double[] yValues { get; set; }
        public int[] layeredIndex { get; set; }
    }

    private bool IsInTheBand(double point, double upLimit, double downLimit)
    {
        return point <= upLimit && point >= downLimit;
    }
    
    private double GetClosestBandLineLower(double yV, double bandHeight)
    {
        double closestBandLine = 0.0f;
        int numBandLine = 1;
        if (yV >= 0)
        {
            while (numBandLine * bandHeight <= yV)
            {
                numBandLine += 1;
            }

            return (numBandLine - 1) * bandHeight;
        }
        else
        {
            while (numBandLine * bandHeight <= -yV)
            {
                numBandLine += 1;
            }

            return (numBandLine - 1) * bandHeight * -1;
        }
    }

    private double GetClosestBandLineHigher(double yV, double bandHeight)
    {
        double closestBandLine = 0.0f;
        int numBandLine = 1;
        if (yV >= 0)
        {
            while (numBandLine * bandHeight <= yV)
            {
                numBandLine += 1;
            }

            return numBandLine * bandHeight;
        }
        else
        {
             while (numBandLine * bandHeight <= -yV)
            {
                numBandLine += 1;
            }

            return numBandLine * bandHeight * -1;
        }
    }

    private xyValues GetJunctionPoint(double[] oDataListX, double[] oDataListY, double bandHeight, double baseline)
    {
        // int numBands = 0;
        int numNegativeBands = 0;

        double previousPointX = 0.0;
        double previousPointY = 0.0;
        double Lower;
        double Higher;
        double tempMaxY = 0.0;
        double tempMinY = 0.0;
        double[] dataListX = new double[oDataListX.Length + 2];
        double[] dataListY = new double[oDataListY.Length + 2];

        dataListX[0] = oDataListX[0];
        dataListY[0] = 0.0 - baseline;

        for (int i = 0; i < oDataListY.Length; i++)
        {
            dataListX[i + 1] = oDataListX[i];
            dataListY[i + 1] = oDataListY[i] - baseline;
            if (dataListY[i + 1] > tempMaxY)
            {
                tempMaxY = dataListY[i + 1];
            }
            if (dataListY[i + 1] < tempMinY)
            {
                tempMinY = dataListY[i + 1];
            }
        }
        dataListX[dataListX.Length - 1] = oDataListX[oDataListX.Length - 1];
        dataListY[dataListY.Length - 1] = 0.0 - baseline;
        
        numNegativeBands = (int)(Math.Abs(tempMinY) / bandHeight) + 1;
        // numBands = (Math.Abs(tempMinY) / bandHeight) + tempMaxY / bandHeight;

        previousPointX = dataListX[0];
        previousPointY = dataListY[0];
        List<double> xValueModified = new List<double>();
        List<double> yValueModified = new List<double>();
        List<int> layeredDataList = new List<int>();
        
        // double Lower0 = GetClosestBandLineLower(dataListY[0], bandHeight);
        // xValueModified.Add(dataListX[0]);
        // yValueModified.Add(Lower0);
        // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);
        // xValueModified.Add(dataListX[0]);
        // yValueModified.Add(dataListY[0]);
        // layeredDataList.Add((int)(Lower0 / bandHeight) + numNegativeBands);

        for (int i = 0; i < dataListX.Length; i++)
        {
            if (dataListY[i] >= 0)
            {
                Lower = GetClosestBandLineLower(dataListY[i], bandHeight);
                Higher = GetClosestBandLineHigher(dataListY[i], bandHeight);
                if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
                {
                    if (dataListY[i] > previousPointY)
                    {
                        int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
                        for (int j = timeAddJunctionPoint; j > 0; j--)
                        {
                            double tempLower = Lower - (j - 1) * bandHeight;
                            double tempHigher = Higher - (j - 1) * bandHeight;
                            double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
                            double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempLower);
                            layeredDataList.Add((int)(tempLower / bandHeight) - 1 + numNegativeBands);
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempLower);
                            layeredDataList.Add((int)(tempHigher / bandHeight) - 1 + numNegativeBands);
                        }
                    }
                    else if (dataListY[i] < previousPointY)
                    {
                        int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
                        for (int j = timeAddJunctionPoint; j > 0; j--)
                        {
                            double tempLower = Lower + (j - 1) * bandHeight;
                            double tempHigher = Higher + (j - 1) * bandHeight;
                            double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
                            double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempHigher);
                            layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempHigher);
                            layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
                        }
                    }
                    xValueModified.Add(dataListX[i]);
                    yValueModified.Add(dataListY[i]);
                    layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
                }
                else
                {
                    xValueModified.Add(dataListX[i]);
                    yValueModified.Add(dataListY[i]);
                    layeredDataList.Add((int)(Lower / bandHeight) + numNegativeBands);
                }
                previousPointX = dataListX[i];
                previousPointY = dataListY[i];
            }
            else
            {
                // negative number！！！！
                Higher = GetClosestBandLineLower(dataListY[i], bandHeight);
                Lower = GetClosestBandLineHigher(dataListY[i], bandHeight);
                if (IsInTheBand(previousPointY, Higher, Lower) ^ IsInTheBand(dataListY[i], Higher, Lower))
                {
                    if (dataListY[i] > previousPointY)
                    {
                        int timeAddJunctionPoint = 1 + (int)((Lower - previousPointY) / bandHeight);
                        for (int j = timeAddJunctionPoint; j > 0; j--)
                        {
                            double tempLower = Lower - (j - 1) * bandHeight;
                            double tempHigher = Higher - (j - 1) * bandHeight;
                            double xPercentage = (dataListY[i] - tempLower) / (dataListY[i] - previousPointY);
                            double xJunctionPoint = dataListX[i] - (dataListX[i] - previousPointX) * xPercentage;
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempLower);
                            layeredDataList.Add((int)((tempLower / bandHeight)) + numNegativeBands - 1);
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempLower);
                            layeredDataList.Add((int)((tempHigher / bandHeight)) + numNegativeBands - 1);
                        }
                    }
                    else if (dataListY[i] < previousPointY)
                    {
                        int timeAddJunctionPoint = 1 + (int)((previousPointY - Higher) / bandHeight);
                        for (int j = timeAddJunctionPoint; j > 0; j--)
                        {
                            double tempLower = Lower + (j - 1) * bandHeight;
                            double tempHigher = Higher + (j - 1) * bandHeight;
                            double xPercentage = (previousPointY - tempHigher) / (previousPointY - dataListY[i]);
                            double xJunctionPoint = previousPointX + (dataListX[i] - previousPointX) * xPercentage;
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempHigher);
                            layeredDataList.Add((int)(tempHigher / bandHeight) + numNegativeBands);
                            xValueModified.Add(xJunctionPoint);
                            yValueModified.Add(tempHigher);
                            layeredDataList.Add((int)(tempLower / bandHeight) + numNegativeBands);
                        }
                    }
                    xValueModified.Add(dataListX[i]);
                    yValueModified.Add(dataListY[i]);
                    layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
                }
                else
                {
                    xValueModified.Add(dataListX[i]);
                    yValueModified.Add(dataListY[i]);
                    layeredDataList.Add((int)(numNegativeBands + Lower / bandHeight));
                }
                previousPointX = dataListX[i];
                previousPointY = dataListY[i];
            }
        }
        // Lower = GetClosestBandLineLower(dataListY[dataListY.Length - 1], bandHeight);
        // xValueModified.Add(dataListX[dataListX.Length - 1]);
        // yValueModified.Add(Lower);
        // if (Lower >= 0)
        // {
        //     layeredDataList.Add((int)(Lower / bandHeight) - 1 + numNegativeBands);
        // }
        // else
        // {
        //     layeredDataList.Add((int)Math.Abs((Lower / bandHeight) - 1));
        // }
        return new xyValues { xValues = xValueModified.ToArray(), yValues = yValueModified.ToArray(), layeredIndex = layeredDataList.ToArray() };
    }

    private void Draw(List<DataMark> dataList, double bandHeight, int[] layeredValuesArray)
    {
        int maxNumLayers = layeredValuesArray.Max() + 1;
        List<Vector3>[] layeredData = new List<Vector3>[maxNumLayers];
        layeredData = LayeringData(dataList, layeredValuesArray);
        ConnectLayerPoints(layeredData);
        CreateFillArea(layeredData);
    }

    private List<Vector3>[] LayeringData(List<DataMark> dataList, int[] layeredValuesArray)
    {
        List<List<Vector3>> layers = new List<List<Vector3>>();
        List<Vector3> originalPoints = new List<Vector3>();
        int numLayers = layeredValuesArray.Max() + 1;
        List<Vector3>[] restructuredData = new List<Vector3>[numLayers];

        foreach (var point in dataList)
        {
            var p = point.GetDataMarkChannel().position / 4.0f;
            Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
            originalPoints.Add(pointOrigin);
        }

        for (int i = 0; i < numLayers; i++)
        {
            List<Vector3> layerPoints = new List<Vector3>();
            for (int j = 0; j < layeredValuesArray.Length; j++)
            {
                if (layeredValuesArray[j] == i)
                {
                    layerPoints.Add(originalPoints[j]);
                }
            }
            List<Vector3> layerPointsTemp = new List<Vector3>(layerPoints);
            if (layerPointsTemp.Count > 0)
            {
                float bottomLimit = layerPointsTemp[0].y;
                for (int j = layerPointsTemp.Count - 2; j >= 0; j--)
                {
                    layerPoints.Add(new Vector3(layerPointsTemp[j].x, bottomLimit, layerPointsTemp[j].z));
                }
                restructuredData[i] = layerPoints;
            }
            else
            {
                restructuredData[i] = layerPoints;
            }
        }

        return restructuredData;
    }

    private void ConnectLayerPoints(List<Vector3>[] dataList)
    {
        foreach (var layer in dataList)
        {
            if (layer.Count != 0)
            {
                ConnectLayerPoints(layer);
            }
        }
    }

    private void ConnectLayerPoints(List<Vector3> dataList)
    {
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        // lineRenderer.transform.SetParent(container.transform);
        
        lineRenderer.positionCount = dataList.Count;
        lineRenderer.startWidth = 0.0002f;
        lineRenderer.endWidth = 0.0002f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.SetPositions(dataList.ToArray());
    }
    
    private void CreateFillArea(List<Vector3>[] dataList)
    {
        Color[] selectedColorsPositive = new Color[] {Color.magenta, Color.red};
        Color[] selectedColorsNegative = new Color[] {Color.cyan, Color.blue};
        double maxV = 0.0;
        double minV = 0.0;

        foreach (var listV in dataList)
        {
            foreach (var V in listV)
            {
                if (V.y > maxV)
                {
                    maxV = V.y;
                }
                if (V.y < minV)
                {
                    minV = V.y;
                }
            }
        }
        for (int k = 0; k < dataList.Length; k++)
        {
            if (dataList[k].Count != 0)
            {
                Vector3[] points = new Vector3[dataList[k].Count];
                double maxLayerV = 0.0;
                double minLayerV = 0.0;
                for (int i = 0; i < dataList[k].Count; i++)
                {
                    points[i] = dataList[k][i] * 4.0f;
                    if (maxLayerV < points[i].y)
                    {
                        maxLayerV = points[i].y;
                    }
                    if (minLayerV > points[i].y)
                    {
                        minLayerV = points[i].y;
                    }
                }

                GameObject fillAreaObject = new GameObject("FillArea");
                fillAreaObject.transform.SetParent(visContainerObject.transform);
                MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
                MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();
                Mesh mesh = new Mesh();
                mesh.vertices = points;
                int[] triangles = new int[(points.Length - 1) * 3];
                int index = 0;
                for (int i = 0; i < points.Length - 1; i++)
                {
                    triangles[index++] = i;
                    triangles[index++] = i + 1;
                    triangles[index++] = points.Length - i - 1;
                }
                mesh.triangles = triangles;
                mesh.RecalculateNormals();
                meshFilter.mesh = mesh;
                meshRenderer.material = new Material(Shader.Find("Standard"));
                meshRenderer.material.color = ScaleColor.GetInterpolatedColor((maxLayerV + minLayerV) / 2, minV, maxV, selectedColorsPositive);
                // switch(selectedcolor)
                // {
                //     case "blue":
                //         meshRenderer.material.color = Color.blue;
                //         break;
                //     case "red":
                //         meshRenderer.material.color = Color.red;
                //         break;
                //     case "yellow":
                //         meshRenderer.material.color = Color.yellow;
                //         break;
                //     case "green":
                //         meshRenderer.material.color = Color.green;
                //         break;
                //     case "black":
                //         meshRenderer.material.color = Color.black;
                //         break;
                //     default:
                //         meshRenderer.material.color = Color.white;
                //         break;
                // }
            }
        }
    }
    private void ConnectPoints(List<DataMark> dataList)
    {
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        
        List<Vector3> points = new List<Vector3>();

        foreach (var point in dataList)
        {
            var p = point.GetDataMarkChannel().position / 4.0f;
            points.Add(p);
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.startWidth = 0.0002f;
        lineRenderer.endWidth = 0.0002f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.SetPositions(points.ToArray());
    }
}