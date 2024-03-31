using UnityEngine;
using UnityEngine.UI;

public class Level_46 : BaseLevel
{
    [Header("Answers")]
    public Button theCat;
    public Button theDog;
    public Button[] theWrong;
    public Canvas parentCanvasOfImageToMove;
    private Vector2 posDogMove;
    private Vector2 posCatMove;

    private int countClick = 0;
    private Vector3 posStartCat, posStartDog;
    [SerializeField] private RectTransform rectDog;
    [SerializeField] private RectTransform rectCat;
    private bool isEnd;

    protected override void Start()
    {
        base.Start();
        //theCat.onClick.AddListener(() => WrongAnswer());
        //theDog.onClick.AddListener(() => WrongAnswer());
        for (int i = 0; i < theWrong.Length; i++)
        {
            theWrong[i].onClick.AddListener(() => WrongAnswer());
        }
        posStartCat = theCat.transform.position;
        posStartDog = theDog.transform.position;
        rectDog = theDog.GetComponent<RectTransform>();
        rectCat = theCat.GetComponent<RectTransform>();

        parentCanvasOfImageToMove = FindObjectOfType<Canvas>();
    }


    bool isTouchCat;
    int idTouch_Cat;
    bool isTouchDog;
    int idTouch_Dog;
    int indexTouch;
    protected override void Update()
    {
        base.Update();

        if (isEnd) return;

        if (Input.touches.Length == 0)
        {
            isTouchCat = false;
            isTouchDog = false;
            indexTouch = 0;
            if (!isEnd)
            {
               // WrongAnswer();
                 theCat.transform.position = posStartCat;
                 theDog.transform.position = posStartDog;
                //isEnd = true;
            }
            //Debug.Log("DDDDDDDDĐ");
            return;
            
        }

       // if (Input.touchCount > 2 && Input.touchCount <= 1) return;

         
        
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                if (!isTouchCat)
                {
                    var distanceCat = Vector2.Distance(Camera.main.WorldToScreenPoint(theCat.transform.position), touch.position);
                    
                    if (distanceCat < 80f)
                    {
                        isTouchCat = true;
                        idTouch_Cat = touch.fingerId;
                        indexTouch++;
                        continue;
                    }
                }

                if (!isTouchDog)
                {
                    var distanceDog = Vector2.Distance(Camera.main.WorldToScreenPoint(theDog.transform.position), touch.position);
                    
                    if (distanceDog < 80f)
                    {
                        isTouchDog = true;
                        idTouch_Dog = touch.fingerId;
                        indexTouch++;
                        continue;
                    }
                }
            }
            
        }

        if (isTouchCat)
        {
            if (Input.GetTouch(idTouch_Cat).phase != TouchPhase.Ended && Input.GetTouch(idTouch_Cat).phase != TouchPhase.Canceled)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.GetTouch(idTouch_Cat).position, parentCanvasOfImageToMove.worldCamera, out posCatMove);
                //var posX = Mathf.Clamp(parentCanvasOfImageToMove.transform.TransformPoint(posCatMove).x, -350, -150);
                //rectCat.position = new Vector3(posX, rectCat.position.y, rectCat.position.z);
                theCat.transform.position = new Vector3(parentCanvasOfImageToMove.transform.TransformPoint(posCatMove).x, theCat.transform.position.y, theCat.transform.position.z);
            }
            else
            {
                isTouchCat = false;
            }
        }

        if (isTouchDog)
        {
            if (Input.GetTouch(idTouch_Dog).phase != TouchPhase.Ended && Input.GetTouch(idTouch_Dog).phase != TouchPhase.Canceled)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.GetTouch(idTouch_Dog).position, parentCanvasOfImageToMove.worldCamera, out posDogMove);
                //var posX = Mathf.Clamp(parentCanvasOfImageToMove.transform.TransformPoint(posDogMove).x, 104, 350);
                //rectDog.position = new Vector3(posX, rectDog.position.y, rectDog.position.z);
                theDog.transform.position = new Vector3(parentCanvasOfImageToMove.transform.TransformPoint(posDogMove).x, theDog.transform.position.y, theDog.transform.position.z);
            }
            else
            {
                isTouchDog = false;
            }
        }

        if (isTouchDog && isTouchCat)
        {
            Debug.Log("Dis " + Vector2.Distance(theDog.transform.position, theCat.transform.position));
            if (Vector2.Distance(theDog.transform.position, theCat.transform.position) > 1.7f)
            {
                RightAnswer();
                isEnd = true;
            }
        }

        //var distanceCat0 = Vector3.Distance(theCat.transform.position, Input.GetTouch(0).position);
        //var distanceCat1 = Vector3.Distance(theCat.transform.position, Input.GetTouch(1).position);
        //var distanceDog0 = Vector3.Distance(theDog.transform.position, Input.GetTouch(0).position);
        //var distanceDog1 = Vector3.Distance(theDog.transform.position, Input.GetTouch(1).position);

        //if(distanceCat0 <= 0.35f)
        //{
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.GetTouch(0).position, parentCanvasOfImageToMove.worldCamera, out posCatMove);
        //    var posX = Mathf.Clamp(parentCanvasOfImageToMove.transform.TransformPoint(posCatMove).x, -350, -150);
        //    rectCat.position = new Vector3(posX, rectCat.position.y, rectCat.position.z);
        //}
        //else if(distanceCat1 <= 0.35f)
        //{
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.GetTouch(1).position, parentCanvasOfImageToMove.worldCamera, out posCatMove);
        //    var posX = Mathf.Clamp(parentCanvasOfImageToMove.transform.TransformPoint(posCatMove).x, -350, -150);
        //    rectCat.position = new Vector3(posX, rectCat.position.y, rectCat.position.z);
        //}

        //if (distanceDog0 <= 0.35f)
        //{
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.GetTouch(0).position, parentCanvasOfImageToMove.worldCamera, out posDogMove);
        //    var posX = Mathf.Clamp(parentCanvasOfImageToMove.transform.TransformPoint(posDogMove).x, 104, 350);
        //    rectDog.position = new Vector3(posX, rectDog.position.y, rectDog.position.z);
        //}
        //else if (distanceDog1 <= 0.35f)
        //{
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvasOfImageToMove.transform as RectTransform, Input.GetTouch(1).position, parentCanvasOfImageToMove.worldCamera, out posDogMove);
        //    var posX = Mathf.Clamp(parentCanvasOfImageToMove.transform.TransformPoint(posDogMove).x, 104, 350);
        //    rectDog.position = new Vector3(posX, rectDog.position.y, rectDog.position.z);
        //}
    }
    
    public override void StartLevel()
    {
        base.StartLevel();
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();
    }

    public override void WrongAnswer()
    {
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        isEnd = true;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        var distance = Vector3.Distance(theCat.transform.position, theDog.transform.position);
        Debug.Log(distance);
        if (distance >= 1.6f)
        {
            RightAnswer();
        }
    }

    public void OnPointDown()
    {
        countClick++;
    }

    public void OnPointUp()
    {
        if (countClick >= 2)
        {
            var distance = Vector3.Distance(theCat.transform.position, theDog.transform.position);
            Debug.Log(distance);
            if(distance >= 1.6f)
            {
                RightAnswer();
            }
            else
            {
                theCat.transform.position = posStartCat;
                theDog.transform.position = posStartDog;
                countClick = 0;
            }
        }
        else
        {
            countClick--;
            theCat.transform.position = posStartCat;
            theDog.transform.position = posStartDog;
        }      
    }

    public void EndDragWrong(RectTransform tran)
    {
        if (tran.transform.localPosition != localPositionWrong)
        {
            WrongAnswer();
            tran.transform.localPosition = localPositionWrong;
        }
    }

    Vector3 localPositionWrong;

    public void BeginDragWrong(RectTransform tran)
    {
        localPositionWrong = tran.transform.localPosition;
    }
}