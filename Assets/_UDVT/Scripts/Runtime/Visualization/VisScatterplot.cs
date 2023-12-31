using System.Linq;
using UnityEngine;

public class VisScatterplot : Vis//NewCode_Group10
{
    public VisScatterplot()
    {
        title = "Scatterplot";

        //Define Data Mark and Tick Prefab
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Sphere");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }

    public override GameObject CreateVis(GameObject container, bool flagMultiple, int dataIndex)
    {
        base.CreateVis(container, flagMultiple, dataIndex);

        //## 01:  Create Axes and Grids

        // X Axis
        visContainer.CreateAxis(dataSets[dataIndex].ElementAt(0).Key, dataSets[0].ElementAt(0).Value, Direction.X);
        visContainer.CreateGrid(Direction.X, Direction.Y);

        // Y Axis
        visContainer.CreateAxis(dataSets[dataIndex].ElementAt(1).Key, dataSets[0].ElementAt(1).Value, Direction.Y);

        // Z Axis
        visContainer.CreateAxis(dataSets[dataIndex].ElementAt(2).Key, dataSets[0].ElementAt(2).Value, Direction.Z);
        visContainer.CreateGrid(Direction.Y, Direction.Z);
        visContainer.CreateGrid(Direction.Z, Direction.X);

        //## 02: Set Remaining Vis Channels (Color,...)
        visContainer.SetChannel(VisChannel.XPos, dataSets[dataIndex].ElementAt(0).Value);
        visContainer.SetChannel(VisChannel.YPos, dataSets[dataIndex].ElementAt(1).Value);
        visContainer.SetChannel(VisChannel.ZPos, dataSets[dataIndex].ElementAt(2).Value);
        visContainer.SetChannel(VisChannel.Color, dataSets[dataIndex].ElementAt(3).Value);

        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab);

        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);

        return visContainerObject;
    }

}
