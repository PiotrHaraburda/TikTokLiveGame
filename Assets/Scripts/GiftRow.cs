using System.Collections;
using TikTokLiveSharp.Events.MessageData.Messages;
using TikTokLiveSharp.Events.MessageData.Objects;
using TikTokLiveUnity.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TikTokLiveUnity.Example
{
    /// <summary>
    /// Displays Gift (with Updates) in ExampleScene
    /// </summary>
    public class GiftRow : MonoBehaviour
    {
        public static bool newRoseSent = false;
        public static bool newGymSent = false;
        public static bool newLollipopSent = false;
        public static bool newPerfumeSent = false;
        private uint oldAmount=0;
        
        #region Properties
        /// <summary>
        /// Gift being Displayed
        /// </summary>
        public TikTokGift Gift { get; private set; }

        /// <summary>
        /// Text displaying Sender-Name
        /// </summary>
        [SerializeField]
        [Tooltip("")]
        private TMP_Text txtUserName;
        /// <summary>
        /// Text displaying Amount sent
        /// </summary>
        [SerializeField]
        [Tooltip("Text displaying Amount sent")]
        private TMP_Text txtAmount;
        /*/// <summary>
        /// Image displaying ProfilePicture of Sender
        /// </summary>
        [SerializeField]
        [Tooltip("Image displaying ProfilePicture of Sender")]
        private Image imgUserProfile;*/
        /// <summary>
        /// Image displaying Icon for Gift
        /// </summary>
        [SerializeField]
        [Tooltip("Image displaying Icon for Gift")]
        private Image imgGiftIcon;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes GiftRow
        /// </summary>
        /// <param name="gift">Gift to Display</param>
        public void Init(TikTokGift gift)
        {
            Gift = gift;
            Gift.OnAmountChanged += AmountChanged;
            Gift.OnStreakFinished += StreakFinished;
            txtUserName.text = $"{Gift.Sender.UniqueId}";
            txtAmount.text = $"{Gift.Amount}x";
            RequestImage(imgGiftIcon, Gift.Gift.Picture);
        }
        
        /// <summary>
        /// Deinitializes GiftRow
        /// </summary>
        private void OnDestroy()
        {
            gameObject.SetActive(false);
            if (Gift == null)
                return;
            Gift.OnAmountChanged -= AmountChanged;
            Gift.OnStreakFinished -= StreakFinished;
        }
        /// <summary>
        /// Updates Gift-Amount if Amount Changed
        /// </summary>
        /// <param name="gift">Gift for Event</param>
        /// <param name="newAmount">New Amount</param>
        private void AmountChanged(TikTokGift gift, uint newAmount)
        {
            if (oldAmount < newAmount)
            {
                StartCoroutine(spawner(newAmount,oldAmount,gift));
            }
            oldAmount = newAmount;
            txtAmount.text = $"{newAmount}x";
        }

        IEnumerator spawner(uint newAmount,uint oldAmount,TikTokGift gift)
        {
            for (int i = 1; i <= newAmount - oldAmount; i++)
            {
                
                if (gift.Gift.Id == 5655)
                {
                    newRoseSent = true;
                }
                else if (gift.Gift.Id == 5760)
                {
                    newGymSent = true;
                }
                else if (gift.Gift.Id == 5657)
                {
                    newLollipopSent = true;
                }
                else if (gift.Gift.Id == 5658)
                {
                    newPerfumeSent = true;
                }
                yield return new WaitForSeconds(1);
            }
        }
        
        /// <summary>
        /// Called when GiftStreaks Ends. Starts Destruction-Timer
        /// </summary>
        /// <param name="gift">Gift for Event</param>
        /// <param name="finalAmount">Final Amount for Streak</param>
        private void StreakFinished(TikTokGift gift, uint finalAmount)
        {
            AmountChanged(gift, finalAmount);
            Destroy(gameObject, 2f);
        }
        /// <summary>
        /// Requests Image from TikTokLive-Manager
        /// </summary>
        /// <param name="img">UI-Image used for display</param>
        /// <param name="picture">Data for Image</param>
        private static void RequestImage(Image img, Picture picture)
        {
            Dispatcher.RunOnMainThread(() =>
            {
                if (TikTokLiveManager.Exists)
                    TikTokLiveManager.Instance.RequestSprite(picture, spr =>
                    {
                        if (img != null && img.gameObject != null && img.gameObject.activeInHierarchy)
                            img.sprite = spr;
                    });
            });
        }
        #endregion
    }
}
