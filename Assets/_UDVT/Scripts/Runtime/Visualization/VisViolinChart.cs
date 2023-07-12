// using System.Linq;
// using System;
// using UnityEngine;
// using System.Collections.Generic;

// public class VisViolinChart : Vis
// {
//     public VisViolinChart()
//     {
//         title = "VisViolin";
//         dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
//         tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
//     }

//     public override GameObject CreateVis(GameObject container)
//     {
//         base.CreateVis(container);

//         double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
//         double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
//         double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();
//         double[] xValuesMirrored = new double[xValues.Length*2];
//         double[] yValuesMirrored = new double[yValues.Length*2];
//         for (int i = 0; i <= xValues.Length - 1; i++)
//         {
//             xValuesMirrored[i] = xValues[i];
//             yValuesMirrored[i] = yValues[i];
//         }
//         for (int i = xValues.Length - 1; i >= 0; i--)
//         {
//             xValuesMirrored[2*xValues.Length - i - 1] = xValues[i];
//             yValuesMirrored[2*xValues.Length - i - 1] = yValues[i] * -1.0f;
//         }

//         //## 01:  Create Axes and Grids
//         // X Axis
//         visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesMirrored, Direction.X);
//         visContainer.CreateGrid(Direction.X, Direction.Y);

//         // Y Axis
//         //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
//         visContainer.CreateAxis("Counter", yValuesMirrored, Direction.Y);

//         //## 02: Set Remaining Vis Channels (Color,...)
//         visContainer.SetChannel(VisChannel.XPos, xValuesMirrored);
//         visContainer.SetChannel(VisChannel.YPos, yValuesMirrored);
//         visContainer.SetChannel(VisChannel.Color, yValuesMirrored);
    
//         double[] sizeList = new double[xValuesMirrored.Length];

//         visContainer.SetChannel(VisChannel.XSize, sizeList);
//         visContainer.SetChannel(VisChannel.YSize, sizeList);
//         visContainer.SetChannel(VisChannel.ZSize, sizeList);
//         //## 03: Draw all Data Points with the provided Channels 
//         visContainer.CreateDataMarks(dataMarkPrefab);
//         // foreach (var i in visContainer.dataAxisList)
//         // {
//         //     Debug.Log(visContainer.GetAxisScale(Direction.X));
//         // }
//         ConnectPoints(visContainer.dataMarkList);
//         DrawOtherSignals(visContainer.dataMarkList);
//         //## 04: Rescale Chart
//         visContainerObject.transform.localScale = new Vector3(width, height, depth);
//         CreateFillArea(visContainer.dataMarkList);

//         return visContainerObject;
//     }
    
//     private void ConnectPoints(List<DataMark> dataList)
//     {
//         GameObject lineObject = new GameObject("LineRenderer");
//         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
//         // lineRenderer.transform.SetParent(container.transform);
        
//         ///# Get a list of closed graph consisting of KDE with the x-axis as the axis of symmetry
//         List<Vector3> points = new List<Vector3>();
//         ///## 01 Get the points already calculated from the KDE calculation
//         double mirrorAxisPosition;
//         foreach (var point in dataList)
//         {
//             var p = point.GetDataMarkChannel().position / 4.0f;
//             Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
//             points.Add(pointOrigin);
//         }
//         ///### 02.1 Get the last point(same as the first point) to close the graph
//         Vector3 pointlast = new Vector3(points[0].x, points[0].y, points[0].z);
//         points.Add(pointlast);

//         lineRenderer.positionCount = points.Count;
//         lineRenderer.startWidth = 0.0001f;
//         lineRenderer.endWidth = 0.0001f;
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

//         lineRenderer.SetPositions(points.ToArray());
//     }
    
//     private void CreateFillArea(List<DataMark> dataList)
//     {
//         // Получаем позиции точек из DataMark и создаем массив Vector3
//         Vector3[] points = dataList.Select(dataMark => dataMark.GetDataMarkChannel().position / 4.0f).ToArray();

//         // Создаем игровой объект для отображения области заполнения
//         GameObject fillAreaObject = new GameObject("FillArea");
//         fillAreaObject.transform.SetParent(visContainerObject.transform); // Делаем его дочерним объектом контейнера

//         // Добавляем компонент MeshFilter и MeshRenderer
//         MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
//         MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();

//         // Создаем новую сетку
//         Mesh mesh = new Mesh();

//         // Устанавливаем вершины сетки
//         mesh.vertices = points;

//         // Создаем треугольники для заполнения площади
//         int[] triangles = new int[(points.Length - 1) * 3];
//         int index = 0;
//         for (int i = 0; i < points.Length - 1; i++)
//         {
//             triangles[index++] = i;
//             triangles[index++] = i + 1;
//             triangles[index++] = points.Length - i - 1;
//         }
//         // triangles[index++] = i;

//         // Устанавливаем треугольники сетки
//         mesh.triangles = triangles;

//         // Вычисляем нормали для освещения
//         mesh.RecalculateNormals();

//         // Присваиваем созданную сетку MeshFilter
//         meshFilter.mesh = mesh;

//         // Устанавливаем материал для заполнения области
//         meshRenderer.material = new Material(Shader.Find("Standard"));
//         meshRenderer.material.color = Color.blue; // Задайте желаемый цвет

//         // Опционально можно добавить другие компоненты, такие как Collider, чтобы область была интерактивной
//         // fillAreaObject.AddComponent<MeshCollider>();
//     }

//     private void DrawOtherSignals(List<DataMark> dataList)
//     {
//         // get median
//         // DrawLineSignal(dataList, 0.5d);
//         DrawSquareSignals(dataList, 0.5d, 0.001f);
//         // get 25%
//         DrawLineSignal(dataList, 0.25d, 0.005f);
//         // get 75%
//         DrawLineSignal(dataList, 0.75d, 0.005f);
//         // connect 25% and 75%
//         var p25 = GetSignalPositions(dataList, 0.25d, 0.005f);
//         Vector3 p25SignalPosition = new Vector3(p25[0].x, (p25[0].y + p25[1].y) / 2.0f, -0.001f);
//         var p75 = GetSignalPositions(dataList, 0.75d, 0.005f);
//         Vector3 p75SignalPosition = new Vector3(p75[0].x, (p75[0].y + p75[1].y) / 2.0f, -0.001f);
//         ConnectSignals(dataList, p25SignalPosition, p75SignalPosition, 0.005f, "black");
        
//         // connect max and min
//         var minV = GetSignalPositions(dataList, 0.0d, 0.005f);
//         Vector3 minVSignalPosition = new Vector3(minV[0].x, (minV[0].y + minV[1].y) / 2.0f, 0.0f);
//         var maxV = GetSignalPositions(dataList, 1.0d, 0.005f);
//         Vector3 maxVSignalPosition = new Vector3(maxV[0].x, (maxV[0].y + maxV[1].y) / 2.0f, 0.0f);
//         ConnectSignals(dataList, minVSignalPosition, maxVSignalPosition, 0.001f, "");
//     }

//     private void DrawSquareSignals(List<DataMark> dataList, double percent, float signalSize)
//     {
//         GameObject lineObject = new GameObject("LineRenderer");
//         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
//         // lineRenderer.transform.SetParent(container.transform);
//         List<Vector3> targetPosition = new List<Vector3>();
        
//         targetPosition = GetSignalPositions(dataList, percent, signalSize);

//         lineRenderer.positionCount = targetPosition.Count;
//         lineRenderer.startWidth = signalSize * 2;
//         lineRenderer.endWidth = signalSize * 2;
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
//         lineRenderer.SetPositions(targetPosition.ToArray());
//     }

//     private void ConnectSignals(List<DataMark> dataList, Vector3 point1, Vector3 point2, float thickness, string selectedcolor)
//     {
//         GameObject lineObject = new GameObject("LineRenderer");
//         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
//         // lineRenderer.transform.SetParent(container.transform);
//         List<Vector3> targetPosition = new List<Vector3>();
        
//         // connect them
//         targetPosition.Add(point1);
//         targetPosition.Add(point2);

//         switch(selectedcolor)
//         {
//             case "blue":
//                 lineRenderer.material.color = Color.blue;
//                 break;
//             case "red":
//                 lineRenderer.material.color = Color.red;
//                 break;
//             case "yellow":
//                 lineRenderer.material.color = Color.yellow;
//                 break;
//             case "green":
//                 lineRenderer.material.color = Color.green;
//                 break;
//             case "black":
//                 lineRenderer.material.color = Color.black;
//                 break;
//             default:
//                 lineRenderer.material.color = Color.white;
//                 break;
//         }
//         lineRenderer.positionCount = targetPosition.Count;
//         lineRenderer.startWidth = thickness;
//         lineRenderer.endWidth = thickness;
//         // lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
//         lineRenderer.SetPositions(targetPosition.ToArray());
//     }

//     private void DrawLineSignal(List<DataMark> dataList, double percent, float signalSize)
//     {
//         GameObject lineObject = new GameObject("LineRenderer");
//         LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
//         // lineRenderer.transform.SetParent(container.transform);
//         List<Vector3> targetPosition = new List<Vector3>();
        
//         targetPosition = GetSignalPositions(dataList, percent, signalSize);

//         lineRenderer.positionCount = targetPosition.Count;
//         lineRenderer.startWidth = 0.001f;
//         lineRenderer.endWidth = 0.001f;
//         lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
//         lineRenderer.SetPositions(targetPosition.ToArray());
//     }

//     private List<Vector3> GetSignalPositions(List<DataMark> dataList, double percent, float signalSize)
//     {
//         List<Vector3> specialPoints = new List<Vector3>();
//         // float signalSize = 0.005f;

//         if ((dataList.Count / 2 * percent) % 1 == 0)
//         {
//             var p = dataList[(int)Math.Max(0, (dataList.Count / 2 * percent - 1))];
//             var px = p.GetDataMarkChannel().position.x / 4.0f;
//             var py = p.GetDataMarkChannel().position.y / 4.0f;
//             var mirrorP = dataList[(int)Math.Min(dataList.Count - 1, dataList.Count - ((int)(dataList.Count / 2 * percent)))];
//             var mirrorPy = mirrorP.GetDataMarkChannel().position.y / 4.0f;

//             Vector3 medianPoint = new Vector3(px, (py + mirrorPy) / 2.0f - signalSize, -0.0015f);
//             specialPoints.Add(medianPoint);
//             Vector3 mirroredMedianPoint = new Vector3(px, (py + mirrorPy) / 2.0f + signalSize, -0.0015f);
//             specialPoints.Add(mirroredMedianPoint);
//         }
//         else
//         {
//             var previousP = dataList[(int)Math.Max(0, ((dataList.Count - 1) / 2 * percent - 1))];
//             var previousPx = previousP.GetDataMarkChannel().position.x / 4.0f;
//             var previousPy = previousP.GetDataMarkChannel().position.y / 4.0f;
//             var latterP = dataList[(int)Math.Max(0, ((dataList.Count + 1) / 2 * percent - 1))];
//             var latterPx = latterP.GetDataMarkChannel().position.x / 4.0f;
            
//             float previousValue = previousPx / 4.0f;
//             float latterValue = latterPx / 4.0f;

//             var mirrorPreviousP = dataList[(int)Math.Min(dataList.Count - 1, dataList.Count - ((int)((dataList.Count - 1) / 2 * percent)))];
//             var mirrorPreviousPy = mirrorPreviousP.GetDataMarkChannel().position.y / 4.0f;

//             Vector3 medianPoint = new Vector3((previousValue + latterValue) / 2, (previousPy + mirrorPreviousPy) / 2.0f - signalSize, -0.0015f);
//             specialPoints.Add(medianPoint);
//             Vector3 mirroredMedianPoint = new Vector3((previousValue + latterValue) / 2, (previousPy + mirrorPreviousPy) / 2.0f + signalSize, -0.0015f);
//             specialPoints.Add(mirroredMedianPoint);
//         }
//         return specialPoints;
//     }
// }

using System.Linq;
using System;
using UnityEngine;
using System.Collections.Generic;

public class VisViolinChart : Vis
{
    public VisViolinChart()
    {
        title = "VisViolin";
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }

    public override GameObject CreateVis(GameObject container)
    {
        base.CreateVis(container);

        double p25PositionX = GetTargetPointX(dataSets[0].ElementAt(0).Value, 0.25);
        double pMedianPositionX = GetTargetPointX(dataSets[0].ElementAt(0).Value, 0.5);
        double p75PositionX = GetTargetPointX(dataSets[0].ElementAt(0).Value, 0.75);

        double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[0].ElementAt(0).Value, 1, 100);
        double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
        double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();
        double[] xValuesMirrored = new double[xValues.Length*2];
        double[] yValuesMirrored = new double[yValues.Length*2];
        for (int i = 0; i <= xValues.Length - 1; i++)
        {
            xValuesMirrored[i] = xValues[i];
            yValuesMirrored[i] = yValues[i];
        }
        for (int i = xValues.Length - 1; i >= 0; i--)
        {
            xValuesMirrored[2*xValues.Length - i - 1] = xValues[i];
            yValuesMirrored[2*xValues.Length - i - 1] = yValues[i] * -1.0f;
        }

        //## 01:  Create Axes and Grids
        // X Axis
        visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, xValuesMirrored, Direction.X);
        // visContainer.CreateGrid(Direction.X, Direction.Y);

        // Y Axis
        //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
        visContainer.CreateAxis("Counter", yValuesMirrored, Direction.Y);

        //## 02: Set Remaining Vis Channels (Color,...)
        visContainer.SetChannel(VisChannel.XPos, xValuesMirrored);
        visContainer.SetChannel(VisChannel.YPos, yValuesMirrored);
        visContainer.SetChannel(VisChannel.Color, yValuesMirrored);
    
        double[] sizeList = new double[xValuesMirrored.Length];

        visContainer.SetChannel(VisChannel.XSize, sizeList);
        visContainer.SetChannel(VisChannel.YSize, sizeList);
        visContainer.SetChannel(VisChannel.ZSize, sizeList);
        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab);
        // foreach (var i in visContainer.dataAxisList)
        // {
        //     Debug.Log(visContainer.GetAxisScale(Direction.X));
        // }
        ConnectPoints(visContainer.dataMarkList);
        DrawOtherSignals(visContainer.dataMarkList);
        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);
        CreateFillArea(visContainer.dataMarkList);

        return visContainerObject;
    }

    private double GetTargetPointX(double[] oDataList, double percent)
    {
        return oDataList[(int)(oDataList.Length * percent)];
    }
    
    private void ConnectPoints(List<DataMark> dataList)
    {
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        // lineRenderer.transform.SetParent(container.transform);
        
        ///# Get a list of closed graph consisting of KDE with the x-axis as the axis of symmetry
        List<Vector3> points = new List<Vector3>();
        ///## 01 Get the points already calculated from the KDE calculation
        double mirrorAxisPosition;
        foreach (var point in dataList)
        {
            var p = point.GetDataMarkChannel().position / 4.0f;
            Vector3 pointOrigin = new Vector3(p.x, p.y, p.z);
            points.Add(pointOrigin);
        }
        ///### 02.1 Get the last point(same as the first point) to close the graph
        Vector3 pointlast = new Vector3(points[0].x, points[0].y, points[0].z);
        points.Add(pointlast);

        lineRenderer.positionCount = points.Count;
        lineRenderer.startWidth = 0.0001f;
        lineRenderer.endWidth = 0.0001f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.SetPositions(points.ToArray());
    }
    
    private void CreateFillArea(List<DataMark> dataList)
    {
        // Получаем позиции точек из DataMark и создаем массив Vector3
        Vector3[] points = dataList.Select(dataMark => dataMark.GetDataMarkChannel().position / 4.0f).ToArray();

        // Создаем игровой объект для отображения области заполнения
        GameObject fillAreaObject = new GameObject("FillArea");
        fillAreaObject.transform.SetParent(visContainerObject.transform); // Делаем его дочерним объектом контейнера

        // Добавляем компонент MeshFilter и MeshRenderer
        MeshFilter meshFilter = fillAreaObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = fillAreaObject.AddComponent<MeshRenderer>();

        // Создаем новую сетку
        Mesh mesh = new Mesh();

        // Устанавливаем вершины сетки
        mesh.vertices = points;

        // Создаем треугольники для заполнения площади
        int[] triangles = new int[(points.Length - 1) * 3];
        int index = 0;
        for (int i = 0; i < points.Length - 1; i++)
        {
            triangles[index++] = i;
            triangles[index++] = i + 1;
            triangles[index++] = points.Length - i - 1;
        }
        // triangles[index++] = i;

        // Устанавливаем треугольники сетки
        mesh.triangles = triangles;

        // Вычисляем нормали для освещения
        mesh.RecalculateNormals();

        // Присваиваем созданную сетку MeshFilter
        meshFilter.mesh = mesh;

        // Устанавливаем материал для заполнения области
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = Color.blue; // Задайте желаемый цвет

        // Опционально можно добавить другие компоненты, такие как Collider, чтобы область была интерактивной
        // fillAreaObject.AddComponent<MeshCollider>();
    }

    private void DrawOtherSignals(List<DataMark> dataList)
    {
        // get median
        // DrawLineSignal(dataList, 0.5d);
        DrawSquareSignals(dataList, 0.5d, 0.001f);
        // get 25%
        DrawLineSignal(dataList, 0.25d, 0.005f);
        // get 75%
        DrawLineSignal(dataList, 0.75d, 0.005f);
        // connect 25% and 75%
        var p25 = GetSignalPositions(dataList, 0.25d, 0.005f);
        Vector3 p25SignalPosition = new Vector3(p25[0].x, (p25[0].y + p25[1].y) / 2.0f, -0.001f);
        var p75 = GetSignalPositions(dataList, 0.75d, 0.005f);
        Vector3 p75SignalPosition = new Vector3(p75[0].x, (p75[0].y + p75[1].y) / 2.0f, -0.001f);
        ConnectSignals(dataList, p25SignalPosition, p75SignalPosition, 0.005f, "black");
        
        // connect max and min
        var minV = GetSignalPositions(dataList, 0.0d, 0.005f);
        Vector3 minVSignalPosition = new Vector3(minV[0].x, (minV[0].y + minV[1].y) / 2.0f, 0.0f);
        var maxV = GetSignalPositions(dataList, 1.0d, 0.005f);
        Vector3 maxVSignalPosition = new Vector3(maxV[0].x, (maxV[0].y + maxV[1].y) / 2.0f, 0.0f);
        ConnectSignals(dataList, minVSignalPosition, maxVSignalPosition, 0.001f, "");
    }

    private void DrawSquareSignals(List<DataMark> dataList, double percent, float signalSize)
    {
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        // lineRenderer.transform.SetParent(container.transform);
        List<Vector3> targetPosition = new List<Vector3>();
        
        targetPosition = GetSignalPositions(dataList, percent, signalSize);

        lineRenderer.positionCount = targetPosition.Count;
        lineRenderer.startWidth = signalSize * 2;
        lineRenderer.endWidth = signalSize * 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetPositions(targetPosition.ToArray());
    }

    private void ConnectSignals(List<DataMark> dataList, Vector3 point1, Vector3 point2, float thickness, string selectedcolor)
    {
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        // lineRenderer.transform.SetParent(container.transform);
        List<Vector3> targetPosition = new List<Vector3>();
        
        // connect them
        targetPosition.Add(point1);
        targetPosition.Add(point2);

        switch(selectedcolor)
        {
            case "blue":
                lineRenderer.material.color = Color.blue;
                break;
            case "red":
                lineRenderer.material.color = Color.red;
                break;
            case "yellow":
                lineRenderer.material.color = Color.yellow;
                break;
            case "green":
                lineRenderer.material.color = Color.green;
                break;
            case "black":
                lineRenderer.material.color = Color.black;
                break;
            default:
                lineRenderer.material.color = Color.white;
                break;
        }
        lineRenderer.positionCount = targetPosition.Count;
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        // lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetPositions(targetPosition.ToArray());
    }

    private void DrawLineSignal(List<DataMark> dataList, double percent, float signalSize)
    {
        GameObject lineObject = new GameObject("LineRenderer");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        // lineRenderer.transform.SetParent(container.transform);
        List<Vector3> targetPosition = new List<Vector3>();
        
        targetPosition = GetSignalPositions(dataList, percent, signalSize);

        lineRenderer.positionCount = targetPosition.Count;
        lineRenderer.startWidth = 0.001f;
        lineRenderer.endWidth = 0.001f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetPositions(targetPosition.ToArray());
    }

    private List<Vector3> GetSignalPositions(List<DataMark> dataList, double percent, float signalSize)
    {
        List<Vector3> specialPoints = new List<Vector3>();
        // float signalSize = 0.005f;
        double totalArea = 0.0;
        double accumulatedArea = 0.0;
        int targetX = 0;
        for (int i = 1; i < (int)(dataList.Count / 2); i++)
        {
            totalArea += (dataList[i].GetDataMarkChannel().position.x - dataList[i - 1].GetDataMarkChannel().position.x) * (dataList[i].GetDataMarkChannel().position.y + dataList[i - 1].GetDataMarkChannel().position.y) / 2;
        }
        for (int i = 1; i < (int)(dataList.Count / 2); i++)
        {
            accumulatedArea += (dataList[i].GetDataMarkChannel().position.x - dataList[i - 1].GetDataMarkChannel().position.x) * (dataList[i].GetDataMarkChannel().position.y + dataList[i - 1].GetDataMarkChannel().position.y) / 2;
            if (accumulatedArea >= totalArea * percent)
            {
                targetX = i;
                break;
            }
        }
        var p = dataList[targetX];
        var px = p.GetDataMarkChannel().position.x / 4.0f;
        var py = p.GetDataMarkChannel().position.y / 4.0f;
        var mirrorP = dataList[(int)Math.Min(dataList.Count - 1, dataList.Count - targetX - 1)];
        var mirrorPy = mirrorP.GetDataMarkChannel().position.y / 4.0f;

        Vector3 medianPoint = new Vector3(px, (py + mirrorPy) / 2.0f - signalSize, -0.0015f);
        specialPoints.Add(medianPoint);
        Vector3 mirroredMedianPoint = new Vector3(px, (py + mirrorPy) / 2.0f + signalSize, -0.0015f);
        specialPoints.Add(mirroredMedianPoint);

        return specialPoints;
    }
}