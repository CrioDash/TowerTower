using UI.Impls;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BuyBlock
{
    public class BuyBlockView : UIView
    {
        [SerializeField] private Button buyButton;

        public Button BuyButton => buyButton;
    }
}