/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MainScript handles the activities needed at the start of the application.
/// </summary>
public class MainScript : MonoBehaviour
{
    private FileLoadingManager fileLoadingManager;
    private Dictionary<string, double[]> dataSet;

    private Vis vis;

    // Awake is called before Start
    void Awake()
    {
        fileLoadingManager = new FileLoadingManager();
    }

    // Start is called at the beginning of the application
    async void Start()
    {
        LoadAndVisualize();
    }

    void Update()
    {
        // If vis is not null, update the grids
        if (vis != null)
        {
            vis.UpdateGrids();
        }
        
    }

    public async void LoadAndVisualize()
    {
        //## 01: Load Dataset

        string filePath = fileLoadingManager.StartPicker();
        // Application waits for the loading process to finish
        FileType file = await fileLoadingManager.LoadDataset();

        if (file == null) return; //Nothing loaded

        //## 02: Process Dataset

        CsvFileType csvFile = (CsvFileType)file;
        dataSet = csvFile.GetDataSet();


        //## 03: Visualize Dataset

        vis = Vis.GetSpecificVisType(VisType.Scatterplot);
        vis = Vis.GetSpecificVisType(VisType.BarChart);
        vis = Vis.GetSpecificVisType(VisType.DensityChart);
        vis = Vis.GetSpecificVisType(VisType.ViolinChart);
        vis = Vis.GetSpecificVisType(VisType.HorizonChart);

        vis.AppendData(dataSet);
        vis.CreateVis(this.gameObject);
    }
}*/

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

/// <summary>
/// MainScript handles the activities needed at the start of the application.
/// </summary>
/*public class MainScript : MonoBehaviour
{
    private FileLoadingManager fileLoadingManager;
    private Dictionary<string, double[]> dataSet;

    private Vis vis1;
    private Vis vis2;
    private Vis vis3;
    private Vis vis4;

    public Transform vis1Parent;
    public Transform vis2Parent;
    public Transform vis3Parent;
    public Transform vis4Parent;

    // Awake is called before Start
    void Awake()
    {
        fileLoadingManager = new FileLoadingManager();
    }

    // Start is called at the beginning of the application
    async void Start()
    {
        LoadAndVisualize();
    }

    void Update()
    {
        // If vis is not null, update the grids
        if (vis1 != null)
        {
            vis1.UpdateGrids();
        }

    }

    public async void LoadAndVisualize()
    {
        //## 01: Load Dataset

        string filePath = fileLoadingManager.StartPicker();
        // Application waits for the loading process to finish
        FileType file = await fileLoadingManager.LoadDataset();

        if (file == null) return; //Nothing loaded

        //## 02: Process Dataset

        CsvFileType csvFile = (CsvFileType)file;
        dataSet = csvFile.GetDataSet();


        //## 03: Visualize Dataset

        // Загрузка и обработка набора данных...

        // Создание родительских объектов для каждой визуализации
        GameObject vis1ParentObj = new GameObject("Vis1 Parent");
        vis1Parent = vis1ParentObj.transform;

        GameObject vis2ParentObj = new GameObject("Vis2 Parent");
        vis2Parent = vis2ParentObj.transform;

        GameObject vis3ParentObj = new GameObject("Vis3 Parent");
        vis3Parent = vis3ParentObj.transform;

        //Создание объектов визуализации и привязка их к соответствующим родительским объектам
        //vis1 = Vis.GetSpecificVisType(VisType.BarChart);
        //vis1.AppendData(dataSet);
        //vis1.CreateVis(vis1ParentObj);

        //vis2 = Vis.GetSpecificVisType(VisType.DensityChart);
        //vis2.AppendData(dataSet);
        //vis2.CreateVis(vis2ParentObj);

        vis3 = Vis.GetSpecificVisType(VisType.ViolinChart);
        vis3.AppendData(dataSet);
        vis3.CreateVis(vis3ParentObj);

        // Установка позиций и размеров родительских объектов
        //vis1Parent.position = new Vector3(0f, 0f, 0f);
        //vis1Parent.localScale = new Vector3(1f, 1f, 1f);

        //vis2Parent.position = new Vector3(0.5f, 0f, 0f);
        //vis2Parent.localScale = new Vector3(1f, 1f, 1f);

        vis3Parent.position = new Vector3(0f, 0.5f, 0f);
        vis3Parent.localScale = new Vector3(1f, 1f, 1f);
    }
}*/




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



/// <summary>
/// MainScript handles the activities needed at the start of the application.
/// </summary>
public class MainScript : MonoBehaviour
{
    private FileLoadingManager fileLoadingManager;
    private Dictionary<string, double[]> dataSet;


    private Button Button1;
    private Button Button2;
    private Button Button3;
    private Button Button4;

    private Vis vis1;
    private Vis vis2;
    private Vis vis3;
    private Vis vis4;

    public Transform vis1Parent;
    public Transform vis2Parent;
    public Transform vis3Parent;
    public Transform vis4Parent;




    private int currentVisIndex = 0;
    private bool isInitialized = false; // Flag to track if initialization has been done




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

        // Add click listeners to the buttons
        Button1.onClick.AddListener(Button1Click);
        Button2.onClick.AddListener(Button2Click);
        Button3.onClick.AddListener(Button3Click);
        Button4.onClick.AddListener(Button4Click);

        Initialize();
    }


    void HandleButtonClick()
    {
        // Execute desired logic
        Debug.Log("Button pressed!");
    }

    void Update()
    {
        // If vis is not null, update the grids
        /*if (currentVisIndex < 0)
        {
            currentVisIndex = 3;
        }
        if (currentVisIndex > 3)
        {
            currentVisIndex = 0;
        }
        if (vis1 != null)
        {
            vis1.UpdateGrids();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.Log("Space key was pressed.");
            currentVisIndex = currentVisIndex + 1;
            LoadAndVisualize();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Log("Space key was pressed.");
            currentVisIndex = currentVisIndex - 1;
            LoadAndVisualize();
        }*/


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

            // Проверить нажатия кнопок в каждом кадре
            // Check for button clicks in each frame
            /*if (Button1 != null && Button1.IsInteractable())
            {
                if (Button1.onClick.GetPersistentEventCount() > 0 && Input.GetMouseButtonDown(0))
                {
                    Debug.Log(currentVisIndex);
                    currentVisIndex = 0;
                    LoadAndVisualize();
                    Debug.Log(currentVisIndex);

                }
            }

            if (Button2 != null && Button2.IsInteractable())
            {
                if (Button2.onClick.GetPersistentEventCount() > 0 && Input.GetMouseButtonDown(0))
                {
                    Debug.Log(currentVisIndex);

                    currentVisIndex = 1;
                    LoadAndVisualize();
                    Debug.Log(currentVisIndex);

                }
            }

            if (Button3 != null && Button3.IsInteractable())
            {
                if (Button3.onClick.GetPersistentEventCount() > 0 && Input.GetMouseButtonDown(0))
                {
                    Debug.Log(currentVisIndex);

                    currentVisIndex = 2;
                    LoadAndVisualize();
                    Debug.Log(currentVisIndex);

                }
            }

            if (Button4 != null && Button4.IsInteractable())
            {
                if (Button4.onClick.GetPersistentEventCount() > 0 && Input.GetMouseButtonDown(0))
                {
                    Debug.Log(currentVisIndex);

                    currentVisIndex = 3;
                    LoadAndVisualize();
                    Debug.Log(currentVisIndex);

                }
            }*/

        }


    }

    private async void Initialize()
    {
        string filePath = fileLoadingManager.StartPicker();
        FileType file = await fileLoadingManager.LoadDataset();

        if (file == null)
        {
            // No file loaded, exit initialization
            return;
        }

        CsvFileType csvFile = (CsvFileType)file;
        dataSet = csvFile.GetDataSet();

        isInitialized = true; // Set initialization flag

        LoadAndVisualize();
    }

    public async void LoadAndVisualize()
    {
        //## 01: Load Dataset


        //string filePath = fileLoadingManager.StartPicker();
        // Application waits for the loading process to finish
        //FileType file = await fileLoadingManager.LoadDataset();

        //if (file == null) return; //Nothing loaded

        //## 02: Process Dataset

        //CsvFileType csvFile = (CsvFileType)file;
        //dataSet = csvFile.GetDataSet();


        //## 03: Visualize Dataset

        // Загрузка и обработка набора данных...

        // Создание родительских объектов для каждой визуализации
        GameObject vis1ParentObj = new GameObject("Vis1 Parent");
        vis1Parent = vis1ParentObj.transform;

        GameObject vis2ParentObj = new GameObject("Vis2 Parent");
        vis2Parent = vis2ParentObj.transform;

        GameObject vis3ParentObj = new GameObject("Vis3 Parent");
        vis3Parent = vis3ParentObj.transform;

        GameObject vis4ParentObj = new GameObject("Vis4 Parent");
        vis4Parent = vis4ParentObj.transform;


        ////Создание объектов визуализации и привязка их к соответствующим родительским объектам
        ////vis1 = Vis.GetSpecificVisType(VisType.BarChart);
        ////vis1.AppendData(dataSet);
        ////vis1.CreateVis(vis1ParentObj);

        ////vis2 = Vis.GetSpecificVisType(VisType.DensityChart);
        ////vis2.AppendData(dataSet);
        ////vis2.CreateVis(vis2ParentObj);

        //vis3 = Vis.GetSpecificVisType(VisType.ViolinChart);
        //vis3.AppendData(dataSet);
        //vis3.CreateVis(vis3ParentObj);

        //// Установка позиций и размеров родительских объектов
        ////vis1Parent.position = new Vector3(0f, 0f, 0f);
        ////vis1Parent.localScale = new Vector3(1f, 1f, 1f);

        ////vis2Parent.position = new Vector3(0.5f, 0f, 0f);
        ////vis2Parent.localScale = new Vector3(1f, 1f, 1f);

        //vis3Parent.position = new Vector3(0f, 0.5f, 0f);
        //vis3Parent.localScale = new Vector3(1f, 1f, 1f);

        //vis1 = Vis.GetSpecificVisType(VisType.BarChart);
        //vis1.AppendData(dataSet);
        //vis1.CreateVis(vis1ParentObj);



        if (currentVisIndex == 0)
        {
            //Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("Vis2 Parent"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));


            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer);
            }



            vis1 = Vis.GetSpecificVisType(VisType.BarChart);
            vis1.AppendData(dataSet);
            vis1.CreateVis(vis1ParentObj);
            //Destroy(vis1ParentObj);
        }

        if (currentVisIndex == 1)
        {
            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("Vis3 Parent"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));


            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer);
            }


            vis2 = Vis.GetSpecificVisType(VisType.DensityChart);
            vis2.AppendData(dataSet);
            vis2.CreateVis(vis2ParentObj);
        }

        if (currentVisIndex == 2)
        {
            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("VisDensity"));
            Destroy(GameObject.Find("LineRenderer"));
            Destroy(GameObject.Find("Vis4 Parent"));
            Destroy(GameObject.Find("VisHorizon"));


            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer);
            }

            vis3 = Vis.GetSpecificVisType(VisType.ViolinChart);
            vis3.AppendData(dataSet);
            vis3.CreateVis(vis3ParentObj);

        }

        if (currentVisIndex == 3)
        {
            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("VisViolin"));
            Destroy(GameObject.Find("LineRenderer"));
            Destroy(GameObject.Find("VisBar"));
            Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("Vis1 Parent"));
            Destroy(GameObject.Find("VisDensity"));

            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
            foreach (LineRenderer lineRenderer in lineRenderers)
            {
                Destroy(lineRenderer);
            }

            vis4 = Vis.GetSpecificVisType(VisType.HorizonChart);
            vis4.AppendData(dataSet);
            vis4.CreateVis(vis4ParentObj);

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


}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;


///// <summary>
///// MainScript handles the activities needed at the start of the application.
///// </summary>
//public class MainScript : MonoBehaviour
//{
//    private FileLoadingManager fileLoadingManager;
//    private Dictionary<string, double[]> dataSet;

//    private Vis vis1;
//    private Vis vis2;
//    private Vis vis3;
//    private Vis vis4;

//    public Transform vis1Parent;
//    public Transform vis2Parent;
//    public Transform vis3Parent;
//    public Transform vis4Parent;

//    private int currentVisIndex = 0;


//    // Awake is called before Start
//    void Awake()
//    {
//        fileLoadingManager = new FileLoadingManager();
//    }

//    // Start is called at the beginning of the application
//    async void Start()
//    {

//        LoadAndVisualize();
//    }

//    void Update()
//    {
//        // If vis is not null, update the grids
//        /*if (currentVisIndex < 0)
//        {
//            currentVisIndex = 3;
//        }
//        if (currentVisIndex > 3)
//        {
//            currentVisIndex = 0;
//        }
//        if (vis1 != null)
//        {
//            vis1.UpdateGrids();
//        }
//        if (Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            //Debug.Log("Space key was pressed.");
//            currentVisIndex = currentVisIndex + 1;
//            LoadAndVisualize();
//        }
//        if (Input.GetKeyDown(KeyCode.LeftArrow))
//        {
//            //Debug.Log("Space key was pressed.");
//            currentVisIndex = currentVisIndex - 1;
//            LoadAndVisualize();
//        }*/
//        if (Input.GetKeyDown(KeyCode.Keypad1))
//        {
//            //Debug.Log("Space key was pressed.");
//            currentVisIndex = 0;
//            LoadAndVisualize();
//        }
//        if (Input.GetKeyDown(KeyCode.Keypad2))
//        {
//            //Debug.Log("Space key was pressed.");
//            currentVisIndex = 1;
//            LoadAndVisualize();
//        }
//        if (Input.GetKeyDown(KeyCode.Keypad3))
//        {
//            //Debug.Log("Space key was pressed.");
//            currentVisIndex = 2;
//            LoadAndVisualize();
//        }
//        if (Input.GetKeyDown(KeyCode.Keypad4))
//        {
//            //Debug.Log("Space key was pressed.");
//            currentVisIndex = 3;
//            LoadAndVisualize();
//        }
//    }

//    public async void LoadAndVisualize()
//    {
//        //## 01: Load Dataset


//        string filePath = fileLoadingManager.StartPicker();
//        // Application waits for the loading process to finish
//        FileType file = await fileLoadingManager.LoadDataset();

//        if (file == null) return; //Nothing loaded

//        //## 02: Process Dataset

//        CsvFileType csvFile = (CsvFileType)file;
//        dataSet = csvFile.GetDataSet();


//        //## 03: Visualize Dataset

//        // Загрузка и обработка набора данных...

//        // Создание родительских объектов для каждой визуализации
//        GameObject vis1ParentObj = new GameObject("Vis1 Parent");
//        vis1Parent = vis1ParentObj.transform;

//        GameObject vis2ParentObj = new GameObject("Vis2 Parent");
//        vis2Parent = vis2ParentObj.transform;

//        GameObject vis3ParentObj = new GameObject("Vis3 Parent");
//        vis3Parent = vis3ParentObj.transform;

//        GameObject vis4ParentObj = new GameObject("Vis4 Parent");
//        vis4Parent = vis4ParentObj.transform;


//        ////Создание объектов визуализации и привязка их к соответствующим родительским объектам
//        ////vis1 = Vis.GetSpecificVisType(VisType.BarChart);
//        ////vis1.AppendData(dataSet);
//        ////vis1.CreateVis(vis1ParentObj);

//        ////vis2 = Vis.GetSpecificVisType(VisType.DensityChart);
//        ////vis2.AppendData(dataSet);
//        ////vis2.CreateVis(vis2ParentObj);

//        //vis3 = Vis.GetSpecificVisType(VisType.ViolinChart);
//        //vis3.AppendData(dataSet);
//        //vis3.CreateVis(vis3ParentObj);

//        //// Установка позиций и размеров родительских объектов
//        ////vis1Parent.position = new Vector3(0f, 0f, 0f);
//        ////vis1Parent.localScale = new Vector3(1f, 1f, 1f);

//        ////vis2Parent.position = new Vector3(0.5f, 0f, 0f);
//        ////vis2Parent.localScale = new Vector3(1f, 1f, 1f);

//        //vis3Parent.position = new Vector3(0f, 0.5f, 0f);
//        //vis3Parent.localScale = new Vector3(1f, 1f, 1f);

//        //vis1 = Vis.GetSpecificVisType(VisType.BarChart);
//        //vis1.AppendData(dataSet);
//        //vis1.CreateVis(vis1ParentObj);



//        if (currentVisIndex == 0)
//        {
//            //Destroy(GameObject.Find("Vis1 Parent"));
//            Destroy(GameObject.Find("Vis2 Parent"));
//            Destroy(GameObject.Find("VisDensity"));
//            Destroy(GameObject.Find("Vis3 Parent"));
//            Destroy(GameObject.Find("Vis4 Parent"));
//            Destroy(GameObject.Find("VisViolin"));
//            Destroy(GameObject.Find("Vis4 Parent"));
//            Destroy(GameObject.Find("VisHorizon"));


//            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
//            foreach (LineRenderer lineRenderer in lineRenderers)
//            {
//                Destroy(lineRenderer);
//            }



//            vis1 = Vis.GetSpecificVisType(VisType.BarChart);
//            vis1.AppendData(dataSet);
//            vis1.CreateVis(vis1ParentObj);
//            //Destroy(vis1ParentObj);
//        }

//        if (currentVisIndex == 1)
//        {
//            Destroy(GameObject.Find("VisBar"));
//            Destroy(GameObject.Find("Vis1 Parent"));
//            Destroy(GameObject.Find("VisViolin"));
//            Destroy(GameObject.Find("Vis3 Parent"));
//            Destroy(GameObject.Find("Vis4 Parent"));
//            Destroy(GameObject.Find("VisHorizon"));


//            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
//            foreach (LineRenderer lineRenderer in lineRenderers)
//            {
//                Destroy(lineRenderer);
//            }


//            vis2 = Vis.GetSpecificVisType(VisType.DensityChart);
//            vis2.AppendData(dataSet);
//            vis2.CreateVis(vis2ParentObj);
//        }

//        if (currentVisIndex == 2)
//        {
//            Destroy(GameObject.Find("VisBar"));
//            Destroy(GameObject.Find("VisDensity"));
//            Destroy(GameObject.Find("LineRenderer"));
//            Destroy(GameObject.Find("Vis4 Parent"));
//            Destroy(GameObject.Find("VisHorizon"));


//            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
//            foreach (LineRenderer lineRenderer in lineRenderers)
//            {
//                Destroy(lineRenderer);
//            }

//            vis3 = Vis.GetSpecificVisType(VisType.ViolinChart);
//            vis3.AppendData(dataSet);
//            vis3.CreateVis(vis3ParentObj);

//        }

//        if (currentVisIndex == 3)
//        {
//            Destroy(GameObject.Find("VisBar"));
//            Destroy(GameObject.Find("VisViolin"));
//            Destroy(GameObject.Find("LineRenderer"));
//            Destroy(GameObject.Find("VisBar"));
//            Destroy(GameObject.Find("Vis1 Parent"));
//            Destroy(GameObject.Find("Vis1 Parent"));
//            Destroy(GameObject.Find("VisDensity"));

//            LineRenderer[] lineRenderers = FindObjectsOfType<LineRenderer>();
//            foreach (LineRenderer lineRenderer in lineRenderers)
//            {
//                Destroy(lineRenderer);
//            }

//            vis4 = Vis.GetSpecificVisType(VisType.HorizonChart);
//            vis4.AppendData(dataSet);
//            vis4.CreateVis(vis4ParentObj);

//        }

//    }


//}
