using System.Linq;
using System;
using UnityEngine;

public class VisBarChart : Vis
{
    //public int[] xyzTicks = { 100, 100, 100 };
    //public virtual void InitVisParams(string visTitle, float width, float height, float depth, float[] xyzOffset, int[] xyzTicks, Color[] colorScheme)
    //{
    //    title = visTitle;
    //    this.width = width;
    //    this.height = height;
    //    this.depth = depth;
    //    this.xyzOffset = xyzOffset;
    //    this.xyzTicks = xyzTicks;
    //    this.colorScheme = colorScheme;
    //}
    // Start is called before the first frame update
    public VisBarChart()
    {
        title = "VisBar";
        dataMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/Marks/Bar");
        tickMarkPrefab = (GameObject)Resources.Load("Prefabs/DataVisPrefabs/VisContainer/Tick");
    }

    public override GameObject CreateVis(GameObject container, bool flagMultiple, int dataIndex)
    {
        base.CreateVis(container, flagMultiple, dataIndex);

        xyValues1 binData = SturgesFormulaBinning_1(dataSets[0].ElementAt(0).Value, "sturges");
        //## 01:  Create Axes and Grids
        //VisBarChart.xyzTicks = { 10, 10, 10 };
        // X Axis
        visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, binData.xValues, Direction.X);
        visContainer.CreateGrid(Direction.X, Direction.Y);

        // Y Axis
        //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues, Direction.Y);
        visContainer.CreateAxis("Counter", binData.yValues, Direction.Y);

        //## 02: Set Remaining Vis Channels (Color,...)
        visContainer.SetChannel(VisChannel.XPos, binData.xValues);
        visContainer.SetChannel(VisChannel.YSize, binData.yValues);
        visContainer.SetChannel(VisChannel.Color, binData.yValues);


        //xyValues2 binData = SturgesFormulaBinning_2(dataSets[0].Values.ToArray(), 0);
        ////## 01:  Create Axes and Grids

        //// X Axis
        //visContainer.CreateAxis(dataSets[0].ElementAt(0).Key, binData.xValues, Direction.X);
        //visContainer.CreateGrid(Direction.X, Direction.Y);

        //// Y Axis
        //visContainer.CreateAxis(dataSets[0].ElementAt(1).Key, binData.yValues[1], Direction.Y);

        ////## 02: Set Remaining Vis Channels (Color,...)
        //visContainer.SetChannel(VisChannel.XPos, binData.xValues);
        //visContainer.SetChannel(VisChannel.YSize, binData.yValues[1]);
        //visContainer.SetChannel(VisChannel.Color, binData.yValues[1]);

        //## 03: Draw all Data Points with the provided Channels 
        visContainer.CreateDataMarks(dataMarkPrefab);

        //## 04: Rescale Chart
        visContainerObject.transform.localScale = new Vector3(width, height, depth);

        return visContainerObject;
    }

    public class xyValues1
    {
        public double[] xValues { get; set; }
        public double[] yValues { get; set; }
    }

    public class xyValues2
    {
        public double[] xValues { get; set; }
        public double[][] yValues { get; set; }
    }

    private xyValues1 SturgesFormulaBinning_1(double[] xArray, string formulaName)
    {
        int binCount = 10;
        if (formulaName == "sturges")
        {
            binCount = SturgesFormula(xArray.Length);
        }
        else if (formulaName == "rice")
        {
            binCount = RiceRule(xArray.Length);
        }
        else if (formulaName == "instruction")
        {
            binCount = SturgesFormula(xArray.Length) + 11;
        }

        xyValues1 binData = method_1(binCount, xArray);

        return binData;
    }

    private int SturgesFormula(int xArrayLength)
    {
        int binCount = (int)(1 + Math.Log(xArrayLength, 2)) + 1;

        return binCount;
    }

    private int RiceRule(int xArrayLength)
    {
        int binCount = (int)(2 * Math.Pow(xArrayLength, 1.0 / 3.0));

        return binCount;
    }

    private xyValues1 method_1(int binCount, double[] xArray)
    {
        double[] processedxArray = new double[binCount];
        double[] processedyArray = new double[binCount];

        Array.Sort(xArray);

        double xMax = xArray[xArray.Length - 1];
        double xMin = xArray[0];

        double xRange = xMax - xMin;
        double binSingleRange = xRange / binCount;

        int bookMark = 0;
        for (int i = 0; i < binCount; i++)
        {
            for (int j = bookMark; j < xArray.Length; j++)
            {
                if (xArray[j] > (xMin + (i + 1) * binSingleRange))
                {
                    bookMark = j;
                    break;
                }
                processedyArray[i] += 1;
            }
            processedxArray[i] = xMin + binSingleRange * (i + 0.5);
        }
        return new xyValues1 { xValues = processedxArray, yValues = processedyArray };
    }

    private xyValues2 SturgesFormulaBinning_2(double[][] xyArray, int xIndex)
    {
        double[] xArray = new double[xyArray.Length];
        double[][] yArray = new double[xyArray.Length - 1][];
        for (int i = 0; i < xyArray.Length - 1; i++)
        {
            yArray[i] = new double[xyArray[0].Length];
        }

        int arrayIndex = 0;
        for (int i = 0; i < xyArray.Length; i++)
        {
            if (i == xIndex)
            {
                xArray = xyArray[i];
                continue;
            }
            yArray[arrayIndex] = xyArray[i];
            arrayIndex++;
        }

        int binCount = SturgesFormula(xArray.Length);

        xyValues2 binData = method_2(binCount, xArray, yArray);

        return binData;
    }

    private xyValues2 method_2(int binCount, double[] xArray, double[][] yArray)
    {
                                                                    // create list to store processed data
        double[][] processedyArray = new double[yArray.Length][];   // processedyArray => [[y11, y12, y13...], [y21, y22, y23...], ...]
        for (int i = 0; i < yArray.Length; i++)                     // double[][] processedyArray = new double[yArray.Length][binCount];
        {
            processedyArray[i] = new double[binCount];
        }
        double[] processedxArray = new double[binCount];            // processedxArray => [x1, x2,...]
        double[][] xyArray = new double[xArray.Length][];           // xyArray => [[x1, y11, y12, y13...], [x2, y21, y22, y23...], ...]
        for (int i = 0; i < xArray.Length; i++)                     // double[][] xyArray = new double[xArray.Length][yArray.Length];
        {
            xyArray[i] = new double[yArray.Length + 1];
        }

        for (int i = 0; i < xArray.Length; i++)
        {
            //xyArray[i][0] = 0.0;
            for (int j = 1; j < yArray.Length + 1; j++)
            {
                xyArray[i][j] = yArray[j - 1][i];
            }
            if (xArray[i] > 0)
            {
                xyArray[i][0] = xArray[i];
            }
        }

        Array.Sort(xyArray, (a, b) => a[0].CompareTo(b[0]));

        double xMax = xyArray[xyArray.Length - 1][0];
        double xMin = xyArray[0][0];

        double xRange = xMax - xMin;
        double binSingleRange = xRange / binCount;

        int bookMark = 0;
        for (int i = 0; i < binCount; i++)
        {
            for (int j = bookMark; j < xyArray.Length; j++)
            {
                if (xyArray[j][0] > (xMin + (i + 1) * binSingleRange))
                {
                    bookMark = j;
                    break;
                }
                for (int k = 0; k < yArray.Length; k++)
                {
                    processedyArray[k][i] += xyArray[j][k + 1];
                }
            }
            for (int j = 0; j < yArray.Length; j++)
            {
                processedyArray[j][i] = processedyArray[j][i] / bookMark;
            }
            processedxArray[i] = xMin + binSingleRange * (i + 0.5);
        }
        return new xyValues2 { xValues = processedxArray, yValues = processedyArray };
    }

}
