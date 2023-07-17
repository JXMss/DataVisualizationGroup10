using System.Linq;
using System;
using UnityEngine;
using System.Collections.Generic;

public class VisDensityChart : Vis//NewCode_Group10
{
    public VisDensityChart()
    {
        title = "VisDensity";
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }

    public override GameObject CreateVis(GameObject container, bool flagMultiple, int dataIndex)
    {
        base.CreateVis(container, flagMultiple, dataIndex);

        double[,] DensityCalculate = KernelDensityEstimation.KDE(dataSets[dataIndex].ElementAt(0).Value, 1, 100);
        double[] xValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 0]).ToArray();
        double[] yValues = Enumerable.Range(0, DensityCalculate.GetLength(0)).Select(i => DensityCalculate[i, 1]).ToArray();

        //## 01:  Create Axes and Grids
        // X Axis
        visContainer.CreateAxis(dataSets[dataIndex].ElementAt(0).Key, xValues, Direction.X);
        visContainer.CreateGrid(Direction.X, Direction.Y);

        // Y Axis
        //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
        visContainer.CreateAxis("KDE", yValues, Direction.Y);

        //## 02: Set Remaining Vis Channels (Color,...)
        visContainer.SetChannel(VisChannel.XPos, xValues);
        visContainer.SetChannel(VisChannel.YPos, yValues);
        visContainer.SetChannel(VisChannel.Color, yValues);

        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab);
        
        ConnectPoints(visContainer.dataMarkList);
        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);

        return visContainerObject;
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
        lineRenderer.startWidth = 0.002f;
        lineRenderer.endWidth = 0.002f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer.SetPositions(points.ToArray());
    }
}