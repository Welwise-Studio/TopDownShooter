using GamePush;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils.ConditionalField;
using AYellowpaper.SerializedCollections;

namespace SimpleTMPLocalization
{
    [RequireComponent(typeof(TMP_Text))]
    public class TMPTranslate : MonoBehaviour
    {
        public static string Lang;


        [SerializeField]
        private SerializedDictionary<string, string> _texts = new ();

        [SerializeField]
        private SerializedDictionary<string, TMP_FontAsset> _overidedFonts = new();

        private TMP_Text _tmp;

        private void Awake()
        {
            Lang = GP_Language.ConvertToString(GP_Language.Current());
            _tmp = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            InitFont();
            InitText();
        }

        private void InitFont() 
        {
            if (_overidedFonts.TryGetValue(Lang, out var overided))
                _tmp.font = overided;
            //else
                //_tmp.font = LocaleData.GetFontAsset(Lang);
        }
        private void InitText()
        {
            if (_texts.TryGetValue(Lang, out var text))
                _tmp.text = text;
            //else
                //_tmp.text = LocaleData.GetText(_idInStorage, Lang);
        }

    }
}
