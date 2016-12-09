using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VRDebug;

namespace VRDebug
{
    public class VRDebugItem : MonoBehaviour {

        public struct Paramator
        {
            public string logText;
            public string stackTraceText;
            public LogType? logType;
        }
        private Image icon;
        private Text log;
        private Text num;
        Paramator paramator;
        private int numCount = 1;
        private Text allText;
        private Image buttonImage;
        [SerializeField]
        private Color[] buttonColors;
        private VRDebugContent content;
        public Color initColor;

        void Awake()
        {
            icon = this.transform.FindChild("Icon").gameObject.GetComponent<Image>();
            log = this.transform.FindChild("TextMask/Text").gameObject.GetComponent<Text>();
            num = this.transform.FindChild("NumText").gameObject.GetComponent<Text>();
            allText = this.transform.Find("../../../AllTextArea/Text").GetComponent<Text>();
            buttonImage = this.GetComponent<Image>();
            content = this.transform.parent.gameObject.GetComponent<VRDebugContent>();
        }

        // Use this for initialization
        void Start() {
            initColor = this.gameObject.GetComponent<Image>().color;
        }

        // Update is called once per frame
        void Update() {
            //if (numCount > 2)
            //{
            //    num.text = numCount.ToString();
            //}
            //else
            //{
            //    num.text = "";
            //}
            num.text = numCount.ToString();
        }

        public void SetParamator(string _log, string _stackTrace, Texture2D _tex, LogType _type, int itemCount)
        {
            paramator.logText = _log;
            paramator.stackTraceText = _stackTrace;
            paramator.logType = _type;

            buttonImage.color = buttonColors[itemCount % 2];

            //ログ反映
            log.text = paramator.logText;
            icon.sprite = Sprite.Create(_tex, new Rect(0, 0, _tex.width, _tex.height), Vector2.zero);
        }

        public Paramator GetParamator()
        {
            return paramator;
        }

        public void AddLogCount()
        {
            numCount++;
        }

        public void OpenText(GameObject item)
        {
            content.SetColor(item);
            allText.text = paramator.logText + "\n" + paramator.stackTraceText;
        }

        public void Initialize()
        {
            paramator.logText = "";
            paramator.stackTraceText = "";
            paramator.logType = null;
            numCount = 1;
        }

        public void SetButtonColor(int count)
        {
            buttonImage.color = buttonColors[count % 2];
        }
    }
}