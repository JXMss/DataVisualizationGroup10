using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



/// <summary>
/// MainScript handles the activities needed at the start of the application.
/// </summary>
public class MainScript : MonoBehaviour//NewCode_Group10
{
    private FileLoadingManager fileLoadingManager;
    private List<Dictionary<string, double[]>> dataSet = new List<Dictionary<string, double[]>>();
    private Dictionary<string, double[]> dataTemp;
    
    private Button Button1;
    private Button Button2;
    private Button Button3;
    private Button Button4;
    private Button Button5;
    private Button Button6;
    private Button Button7;

    private Vis vis1;
    private Vis vis2;
    private Vis vis3;
    private Vis vis4;
    private Vis vis5;

    public Transform vis1Parent;
    public Transform vis2Parent;
    public Transform vis3Parent;
    public Transform vis4Parent;
    public Transform vis5Parent;

    private int currentVisIndex = 0;
    private bool isInitialized = false; // Flag to track if initialization has been done
    private int datasetIndexES = 0;
    private int datasetIndex = 0;

    // Awake is called before Start
    void Awake()
    {
        fileLoadingManager = new FileLoadingManager();
    }

    // Start is called at the beginning of the application
    async void Start()
    {
        Button1 = GameObject.Find("Button1").GetComponent<Button>();
        Button2 = GameObject.Find("Button2").GetComponent<Button>();
        Button3 = GameObject.Find("Button3").GetComponent<Button>();
        Button4 = GameObject.Find("Button4").GetComponent<Button>();
        Button5 = GameObject.Find("Button5").GetComponent<Button>();
        Button6 = GameObject.Find("Button6").GetComponent<Button>();
        Button7 = GameObject.Find("Button7").GetComponent<Button>();



        // Add click listeners to the buttons
        Button1.onClick.AddListener(Button1Click);
        Button2.onClick.AddListener(Button2Click);
        Button3.onClick.AddListener(Button3Click);
        Button4.onClick.AddListener(Button4Click);
        Button5.onClick.AddListener(Button5Click);
        Button6.onClick.AddListener(Button6Click);
        Button7.onClick.AddListener(Button7Click);



        Initialize();
    }


    void HandleButtonClick()
    {
        // Execute desired logic
        Debug.Log("Button pressed!");
    }

    void Update()
    {
        if (isInitialized)
        {
            // Update code when initialization is done
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                currentVisIndex = 0;
                LoadAndVisualize();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                currentVisIndex = 1;
                LoadAndVisualize();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                currentVisIndex = 2;
                LoadAndVisualize();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                currentVisIndex = 3;
                LoadAndVisualize();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                currentVisIndex = 4;
                LoadAndVisualize();
            }
        }


    }

    private async void Initialize()
    {
        string filePath1 = ".\\cars.csv";
        FileType file1 = await fileLoadingManager.LoadDataset(filePath1);

        if (file1 == null)
        {
            // No file loaded, exit initialization
            return;
        }

        CsvFileType csvFile = (CsvFileType)file1;
        dataTemp = csvFile.GetDataSet();
        dataSet.Add(dataTemp);

        string filePath2 = ".\\flowers.csv";
        FileType file2 = await fileLoadingManager.LoadDataset(filePath2);

        if (file2 == null)
        {
            return;
        }
        csvFile = (CsvFileType)file2;
        dataTemp = csvFile.GetDataSet();
        dataSet.Add(dataTemp);

        isInitialized = true; // Set initialization flag

        LoadAndVisualize();
    }
    

    public async void LoadAndVisualize()
    {
        Debug.Log(datasetIndex);


        if (datasetIndex == 0)
        {
            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }
            datasetIndex = 0;

            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));
            Destroy(GameObject.Find("Vis5 Parent"));


        }

        if (datasetIndex == 1)
        {
            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }

            datasetIndex = 1;

            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));
            Destroy(GameObject.Find("Vis5 Parent"));


        }

        if (currentVisIndex == 0)
        {
            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }

            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("Vis1 Parent"));

            GameObject vis1ParentObj = new GameObject("Vis1 Parent");
            vis1Parent = vis1ParentObj.transform;

            //Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));
            Destroy(GameObject.Find("Vis5 Parent"));



            vis1 = Vis.GetSpecificVisType(VisType.BarChart);
            foreach (var dt in dataSet)
            {
                vis1.AppendData(dt);
            }
            vis1.CreateVis(vis1ParentObj, false, datasetIndex);
        }

        if (currentVisIndex == 1)
        {
            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }

            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("VisDensity"));

            GameObject vis2ParentObj = new GameObject("Vis2 Parent");
            vis2Parent = vis2ParentObj.transform;

            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));
            Destroy(GameObject.Find("Vis5 Parent"));



            vis2 = Vis.GetSpecificVisType(VisType.DensityChart);
            foreach (var dt in dataSet)
            {
                vis2.AppendData(dt);
            }
            vis2.CreateVis(vis2ParentObj, false, datasetIndex);
        }

        if (currentVisIndex == 2)
        {
            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }

            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("Vis3 Parent"));

            GameObject vis3ParentObj = new GameObject("Vis3 Parent");
            vis3Parent = vis3ParentObj.transform;

            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("LineRenderer"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));
            Destroy(GameObject.Find("Vis5 Parent"));



            vis3 = Vis.GetSpecificVisType(VisType.ViolinChart);
            foreach (var dt in dataSet)
            {
                vis3.AppendData(dt);
            }
            
            vis3.CreateVis(vis3ParentObj, false, datasetIndex);

        }

        if (currentVisIndex == 3)
        {
            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }

            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));

            GameObject vis4ParentObj = new GameObject("Vis4 Parent");
            vis4Parent = vis4ParentObj.transform;

            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("LineRenderer"));
            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("Vis5 Parent"));



            vis4 = Vis.GetSpecificVisType(VisType.HorizonChart);
            foreach (var dt in dataSet)
            {
                vis4.AppendData(dt);
            }
            
            vis4.CreateVis(vis4ParentObj, false, datasetIndex);
        }

        if (currentVisIndex == 4)
        {
            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer.gameObject);
            }


            Destroy(GameObject.Find("Vis5 Parent"));
            Destroy(GameObject.Find("VisHorizon"));

            GameObject vis5ParentObj = new GameObject("Vis5 Parent");
            vis5Parent = vis5ParentObj.transform;

            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("LineRenderer"));
            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("VisHorizon"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("Vis4 Parent"));
            //Destroy(GameObject.Find("Vis5 Parent"));



            vis5 = Vis.GetSpecificVisType(VisType.HorizonChart);
            foreach (var dt in dataSet)
            {
                vis5.AppendData(dt);
            }
            
            vis5.CreateVis(vis5ParentObj, true, datasetIndex);
        }
    }

    private void Button1Click()
    {
        currentVisIndex = 0;
        LoadAndVisualize();
    }

    private void Button2Click()
    {
        currentVisIndex = 1;
        LoadAndVisualize();
    }

    private void Button3Click()
    {
        currentVisIndex = 2;
        LoadAndVisualize();
    }

    private void Button4Click()
    {
        currentVisIndex = 3;
        LoadAndVisualize();
    }
    
    private void Button5Click()
    {
        currentVisIndex = 4;
        LoadAndVisualize();
    }

    private void Button6Click()
    {
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
        datasetIndex = 0;

        Destroy(GameObject.Find("Vis2 Parent"));
        Destroy(GameObject.Find("VisDensity"));
        Destroy(GameObject.Find("Vis3 Parent"));
        Destroy(GameObject.Find("Vis4 Parent"));
        Destroy(GameObject.Find("VisViolin"));
        Destroy(GameObject.Find("Vis4 Parent"));
        Destroy(GameObject.Find("VisHorizon"));
        Destroy(GameObject.Find("Vis5 Parent"));

        LoadAndVisualize();
    }

    private void Button7Click()
    {
        LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            Destroy(lineRenderer.gameObject);
        }
        datasetIndex = 1;

        Destroy(GameObject.Find("Vis2 Parent"));
        Destroy(GameObject.Find("VisDensity"));
        Destroy(GameObject.Find("Vis3 Parent"));
        Destroy(GameObject.Find("Vis4 Parent"));
        Destroy(GameObject.Find("VisViolin"));
        Destroy(GameObject.Find("Vis4 Parent"));
        Destroy(GameObject.Find("VisHorizon"));
        Destroy(GameObject.Find("Vis5 Parent"));

        LoadAndVisualize();
    }

}