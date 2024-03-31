public class Level_34 : BaseLevel
{
    public Level_34_Player player;
    private bool isEnd;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnMoveUp()
    {
        if(isEnd) return;
        
        player.MoveUp();
    }
    
    public void OnMoveDown()
    {
        if(isEnd) return;
        
        player.MoveDown();
    }
    
    public void OnMoveLeft()
    {
        if(isEnd) return;
        
        player.MoveLeft();
    }
    
    public void OnMoveRight()
    {
        if(isEnd) return;
        
        player.MoveRight();
    }

    public void CheckAnswer()
    {
        isEnd = true;
        RightAnswer();
    }
}