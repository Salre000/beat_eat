using System.Collections.Generic;
using UnityEngine;
using static CreateTapArea;
public class DessertManager : MonoBehaviour
{
    public const float TAP_AREA_DESSERT = -4.0f;

    private const float dessertOffSet = 0;

    private static List<float> timeCount = new List<float>();
    private static List<MeshRenderer> tapPoint = new List<MeshRenderer>();
    [SerializeField] private static List<BoxArea> box = new List<BoxArea>();

    private static List<MeshRenderer> flashLine = new List<MeshRenderer>();
    private static List<Material> flashMaterial = new List<Material>();

    private static List<GameObject> areaList = new List<GameObject>();
    public static GameObject GetAreaList(int index) {  return areaList[index]; }
    [SerializeField] private List<DessertNotes> AllNotes = new List<DessertNotes>();
    public void AddAllNotes(DessertNotes notes) { AllNotes.Add(notes); }
    public void SbuAllNotes(DessertNotes notes) { AllNotes.Remove(notes); }
    public List<DessertNotes> GetAllNotes() { return AllNotes; }
    [SerializeField] private List<DessertNotes> ActiveNotes = new List<DessertNotes>();
    public void AddActiveNotes(DessertNotes notes) { ActiveNotes.Add(notes); }
    public void SbuActiveNotes(DessertNotes notes) { ActiveNotes.Remove(notes); }
    public List<DessertNotes> GetActiveNotes() { return ActiveNotes; }

    private Vector3 startangle;
    private GameObject notesParent;
    public void SetNotesParent(GameObject gameObject) {  notesParent = gameObject; }
    private GameObject areaParent; 
    private bool rotetoFlag = false;

    private int rotetoCount = 0;
    public int GetRotetoCount() {  return rotetoCount; }
    private int rotetoRate = 1;
    //回転させる関数
    public void StartRoteto(int rete) 
    {
        startangle = notesParent.transform.eulerAngles;
        rotetoFlag = true;
        t = 0;
        rotetoRate=rete;
        rotetoCount++;
    }
    private float t = 0;
    private void Roteto() 
    {
        if (!rotetoFlag) return;
        t += Time.deltaTime;


        notesParent.transform.eulerAngles = Vector3.Lerp(startangle, startangle + new Vector3(0, 0, 180f* rotetoRate), t);
        areaParent.transform.eulerAngles = Vector3.Lerp(startangle, startangle + new Vector3(0, 0, 180f * rotetoRate), t);
        areaParent.transform.parent.transform.eulerAngles = 
            Vector3.Lerp(Vector3.zero,new Vector3(0, 0, 30f * rotetoRate)
            , t*2<1?t*2:1f-(t*2-1f));

        if (t < 1) return;
        rotetoFlag = false;    

    }

    public static Material normal;
    public static Material click;
    public static Material flash;

    private const float size = 5;
    public static void CreateTapAreaDessert()
    {
        areaList.Clear();
        box.Clear();
        timeCount.Clear();
        tapPoint.Clear();
        flashLine.Clear();
        flashMaterial.Clear();

        GameObject area = GameObject.Find("Area");

        GameObject dessertArea = new GameObject("DessertObject");
        dessertArea.transform.parent = area.transform;
        dessertArea.transform.localPosition = Vector3.zero;

        //タップするエリアとタップしたら光るエリアを生成
        for (int i = -1; i < 2; i += 2)
        {
            timeCount.Add(0);

            GameObject areaCopy = GameObject.Instantiate(area);

            areaList.Add(areaCopy);
            //いらない部分を破壊
            Destroy(areaCopy.transform.GetChild(0).gameObject);


            areaCopy.transform.position = new Vector3(6 * i, 2, 15);
            areaCopy.transform.eulerAngles = new Vector3(0, 0, 90 * i);
            areaCopy.transform.localScale = new Vector3(0.2f, 1, 8);


            BoxArea boxarea = new BoxArea();
            //メッシュの座標を設定
            boxarea.leftTop = new Vector3(-size, 0, areaRange);
            boxarea.rightTop = new Vector3(size, 0, areaRange);
            boxarea.bottomLeft = new Vector3(-size, 0, -areaRange);
            boxarea.bottomRight = new Vector3(size, 0, -areaRange);

            GameObject go = new GameObject("デザートタップエリア");
            //ここの値が判定の位置

            go.transform.parent = (areaCopy.transform);
            go.transform.Rotate(0, 0, i * 90);
            go.transform.localPosition = Vector3.zero;
            go.transform.position += (go.transform.up / 100f) + new Vector3(0, 0, -go.transform.position.z + TAP_AREA_DESSERT);

            //メッシュの基本設定
            Mesh mesh = new Mesh();
            mesh.vertices = VerticePosition(boxarea);
            mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };


            // 領域と法線を自動で再計算する
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();


            // MeshFilterに設定
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = normal;

            tapPoint.Add(go.GetComponent<MeshRenderer>());

            MeshFilter meshFilter = go.GetComponent<MeshFilter>();


            BoxArea box1 = new BoxArea();

            box1.leftTop = -go.transform.right + go.transform.position + go.transform.forward * 2;
            box1.rightTop = go.transform.right + go.transform.position + go.transform.forward * 2;
            box1.bottomLeft = -go.transform.right + go.transform.position - go.transform.forward * 2;
            box1.bottomRight = go.transform.right + go.transform.position - go.transform.forward * 2;


            box.Add(box1);


            
            boxarea = new BoxArea();
            //メッシュの座標を設定
            boxarea.leftTop = new Vector3(-size, 0, areaRange * 100);
            boxarea.rightTop = new Vector3(size, 0, areaRange * 100);
            boxarea.bottomLeft = new Vector3(-size, 0, areaRange);
            boxarea.bottomRight = new Vector3(size, 0, areaRange);

            go = new GameObject("デザートフラッシュエリア");
            //ここの値が判定の位置

            go.transform.parent = (areaCopy.transform);
            go.transform.Rotate(0, 0, i * 90);
            go.transform.localPosition = Vector3.zero;
            go.transform.position += (go.transform.up / 100f) + new Vector3(0, 0, -go.transform.position.z + TAP_AREA_DESSERT);

            //メッシュの基本設定
            mesh = new Mesh();
            mesh.vertices = VerticePosition(boxarea);
            mesh.triangles = new[] { 0, 1, 3, 3, 1, 2 };


            // 領域と法線を自動で再計算する
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();

            flashMaterial.Add(new Material(flash));
            // MeshFilterに設定
            go.AddComponent<MeshFilter>().mesh = mesh;
            go.AddComponent<MeshRenderer>().material = flashMaterial[i < 0 ? 0 : 1];

            go.transform.parent = areaList[i<0?0:1].transform;
            flashLine.Add(go.GetComponent<MeshRenderer>());




        }

        //二つのオブジェクトの中心に配置
        Vector3 pos = areaList[0].transform.position + (areaList[1].transform.position - areaList[0].transform.position)/2;
        dessertArea.transform.position = pos;

        areaList[0].transform.parent = dessertArea.transform;
        areaList[1].transform.parent = dessertArea.transform;

        DessertManager dessertManager= area.AddComponent<DessertManager>();

        dessertManager.areaParent = dessertArea;

    }

    public void GetClickPoint(Vector2 clickPoint, int id)
    {

        for (int i = 0; i < tapPoint.Count; i++)
        {

            Vector2[] vecs = new Vector2[4];

            for (int j = 0; j < 4; j++)
            {
                //周りの方向ベクトルを取得
                vecs[j] = TapAreaPoint((i+rotetoCount)%2, j);
            }
            bool flag = false;
            for (int j = 0; j < 4; j++)
            {
                //クリックした方向ベクトルを取得
                Vector2 vec = clickPoint - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(box[(i + rotetoCount) % 2])[j]);
                //外積を取得
                Vector3 dont = Vector3.Cross(vecs[j], vec);

                if (dont.z > 0) flag = true;

            }

            if (flag) continue;
            tapPoint[i].material = click;
            timeCount[i] = 1;
            //範囲内をクリックしたと認める
            DessertUtility.Click(i, id);

            return;

        }

    }
    private Vector2 TapAreaPoint(int i, int j)
    {
        return (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(box[i])[(j + 1) % 4])
            - (Vector2)Camera.main.WorldToScreenPoint(VerticePosition(box[i])[j]);

    }

    public void Awake()
    {
        
        DessertUtility.dessertGame = this;
    }
    public void CheckClick()
    {
        for (int i = 0; i < timeCount.Count; i++)
        {
            if (timeCount[i] < 1) continue;
            timeCount[i] += Time.deltaTime;
            if (timeCount[i] < MaxTime) continue;

            timeCount[i] = 0;
            tapPoint[i].material = normal;
        }

    }
    private void FixedUpdate()
    {
        CheckClick();
        SbuAlpha();
        Roteto();
    }

    public void SbuAlpha()
    {
        for (int i = 0; i < flashLine.Count; i++)
        {
            Color color = flashMaterial[i].color;

            if (color.a <= 0) continue;

            color.a -= 0.01f;
            flashMaterial[i].color = color;
        }
    }
    public void AddAlpha(int index)
    {

        Color color = flashMaterial[index].color;

        color.a = 0.2f;
        flashMaterial[index].color = color;
    }


}
