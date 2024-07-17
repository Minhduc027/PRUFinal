using UnityEngine;

public class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private bool notDestroy = false;
    static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType<T>();
                if (m_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    m_instance = singleton.AddComponent<T>();
                }
            }
            return m_instance;
        }
    }

    public virtual void Awake()
    {
        if (notDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }

        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void OnDestroy()
    {
    }
}