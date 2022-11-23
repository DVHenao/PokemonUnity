


public enum EnemyState
{
    NORMAL,
    LOW_HEALTH,
    DEAD,
}


public class Enemy : Character
{
    public BattleManager battleManager;
    public EnemyState currentState;

    private EnemyAIController aIcontroller;
    // Start is called before the first frame update
    void Start()
    {
        aIcontroller = new EnemyAIController(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (status.HP <= 0)
        {
            currentState = EnemyState.DEAD;
        }
        else if (status.HP <= 23.0f)
        {
            currentState = EnemyState.LOW_HEALTH;
        }
        else
        {
            currentState = EnemyState.NORMAL;
        }
    }

    public void ExcuteAI()
    {
        aIcontroller.ExcuteAI();

    }
}
