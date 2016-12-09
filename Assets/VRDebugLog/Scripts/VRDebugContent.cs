using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using VRDebug;

namespace VRDebug
{
    public class VRDebugContent : MonoBehaviour
    {
        public struct ItemParam
        {
            public GameObject itemObj;
            public Color initColor;
            public Image image;
        }
        private List<ItemParam> itemList;
        [SerializeField]
        private Color selectColor;

        void Awake()
        {
            itemList = new List<ItemParam>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddItem()
        {

        }

        public void SetColor(GameObject selectItemObj)
        {
            var itemImage = selectItemObj.GetComponent<Image>();
            var itemComp = selectItemObj.GetComponent<VRDebugItem>();
            //初期化
            itemList = new List<ItemParam>();
            foreach (Transform child in transform)
            {
                var image = child.gameObject.GetComponent<Image>();
                var item = child.gameObject.GetComponent<VRDebugItem>();
                if (image != null)
                {
                    var itemParam = new ItemParam();
                    itemParam.itemObj = child.gameObject;
                    itemParam.image = image;
                    itemParam.initColor = item.initColor;
                    itemList.Add(itemParam);
                }
            }
            //選択されたオブジェクトのみ青色に
            foreach (ItemParam item in itemList)
            {
                if (item.itemObj == selectItemObj)
                {
                    item.image.color = selectColor;
                }
                else
                {
                    item.image.color = item.initColor;
                }
            }
        }
    }
}