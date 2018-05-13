namespace VRTK.Examples
{
    using UnityEngine;

    public class GunMarker : VRTK_InteractableObject
    {
        private GameObject bullet;
        public SceneObjectTracking sot;
        int pool;


        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            base.StartUsing(usingObject);
            FireBullet();
        }

        protected void Start()
        {
            bullet = transform.Find("Bullet").gameObject;
            bullet.SetActive(false);
            pool = 0;
        }

        private void FireBullet()
        {
            if (pool == sot.TrackedItem.Length)
            {
                pool = 0;
            }
            //GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation) as GameObject;
            //bulletClone.SetActive(true);
            sot.TrackedItem[pool].transform.position = bullet.transform.position;
            sot.TrackedItem[pool].transform.localScale = bullet.transform.localScale;
            sot.TrackedItem[pool].transform.eulerAngles = bullet.transform.eulerAngles;
            sot.TrackedItem[pool].SetActive(true);
            pool++;
        }
    }
}