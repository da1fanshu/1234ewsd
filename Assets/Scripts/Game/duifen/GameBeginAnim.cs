﻿using System.Collections;
using UnityEngine;
// ReSharper disable UnusedMember.Local

namespace Assets.Scripts.Game.duifen
{
    public class GameBeginAnim : MonoBehaviour
    {

        public System.Action Finish = null;

        /// <summary>
        /// 动画展示的物体(图片)
        /// </summary>
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private GameObject _labelTex = null;

        /// <summary>
        /// 播放速度
        /// </summary>
        [SerializeField]
        private float speed = 5;

        void OnEnable()
        {
            StartCoroutine(ShowPic());
        }


        /// <summary>
        /// 动画,先展开背景,再显示内容,再淡出
        /// </summary>
        /// <returns></returns>
        IEnumerator ShowPic()
        {
            UIWidget[] objs = transform.GetComponentsInChildren<UIWidget>();
            if (_labelTex != null)
            {
                _labelTex.SetActive(false);
            }
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < objs.Length; i++)
            {
                objs[i].alpha = 1;
            }

            float scaleX = 0;
            gameObject.transform.localScale = new Vector3(0, 1, 1);
            while (scaleX < 1)
            {
                scaleX += Time.deltaTime * speed;
                if (scaleX >= 1)
                {
                    scaleX = 1;
                }
                gameObject.transform.localScale = new Vector3(scaleX, 1, 1);

                yield return null;
            }
            if (_labelTex != null)
            {
                _labelTex.SetActive(true);
            }
            float alpha = 1;

            while (alpha > 0)
            {
                alpha -= Time.deltaTime;
                // ReSharper disable once ForCanBeConvertedToForeach
                for (int i = 0; i < objs.Length; i++)
                {
                    if (alpha <= 0)
                    {
                        alpha = 0;
                    }
                    objs[i].alpha = alpha;

                }
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
            if (Finish != null)
            {
                Finish();
            }
            yield return new WaitForEndOfFrame();
            gameObject.SetActive(false);
        }

        void OnDisable()
        {
            StopCoroutine(ShowPic());
        }

    }
}
