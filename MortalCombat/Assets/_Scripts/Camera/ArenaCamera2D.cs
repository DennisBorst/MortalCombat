using MortalCombat.CameraSystem;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.CameraSystem
{
    [DefaultExecutionOrder(-1000), DisallowMultipleComponent]
    public class ArenaCamera2D : MonoBehaviour
    {
        Camera camera;

        [SerializeField] List<GameObject> _ObjectsToFollow;

        [SerializeField] float _MinimumSize = 5;
        [SerializeField] float _MaximumSize = 15;

        private float
            DEB_currentSize,
            DEB_targetSize,
            DEB_marginX,
            DEB_marginY,
            DEB_XDiff,
            Deb_YDiff;

        private Rect FollowObjectBounds => RendererUtils.Get2DBoundingRect(_ObjectsToFollow);

        [SerializeField] BoundsRestriction _BoundsRestriction = null;

        void Start()
        {
            camera = GetComponent<Camera>();
            _BoundsRestriction = GetComponent<BoundsRestriction>();
        }

        void Update()
        {
            // Get current zoom target
            Rect cameraRect = camera.Get2DCameraRect();
            Rect objectBounds = FollowObjectBounds;

            float zoomTarget = GetTargetZoom(cameraRect, objectBounds, _BoundsRestriction);

            // get current position target
            Vector2 position = GetTargetPositionClamped(_BoundsRestriction, CameraUtils.GetOrtographicSizeVec2D(zoomTarget, camera.aspect));


            // update positions
            UpdateCameraZoom(cameraRect, objectBounds, zoomTarget);
            UpdateCameraPosition(cameraRect, position);
        }

        private Vector2 GetTargetPositionClamped(BoundsRestriction boundsRestriction, Vector2 targetSize)
        {
            Vector2 targetPosition = GetTargetPosition();

            if (!boundsRestriction)
                return targetPosition;

            Rect bounds = boundsRestriction.Bounds;
            Rect rect = new Rect(targetPosition - (targetSize * 0.5f), targetSize);

            float xDiff = 0, yDiff = 0;

            xDiff = Mathf.Max(0, bounds.xMin - rect.xMin) + Mathf.Min(0, bounds.xMax - rect.xMax);
            yDiff = Mathf.Max(0, bounds.yMin - rect.yMin) + Mathf.Min(0, bounds.yMax - rect.yMax);

            DEB_XDiff = xDiff;
            Deb_YDiff = yDiff;
            return targetPosition + new Vector2(xDiff, yDiff);
        }

        private Vector2 GetTargetPosition()
        {
            Vector2 position = Vector2.zero;
            foreach (var item in _ObjectsToFollow)
            {
                position += (Vector2)item.transform.position;
            }
            position /= _ObjectsToFollow.Count;
            return position;
        }

        private void UpdateCameraZoom(Rect cameraRect, Rect objectBounds, float newSizeTarget)
        {
            float size = camera.orthographicSize;
            float difference = newSizeTarget - size;

            float zoomSpeed;
            if (difference < 0)
            {
                zoomSpeed = Mathf.Max(
                    Mathf.Abs(difference),
                    Mathf.Pow(difference, 2));
            }
            else
            {
                zoomSpeed = Mathf.Max(
                    Mathf.Abs(difference),
                    Mathf.Pow(difference, 2),
                    Mathf.Pow(difference, 2.5f),
                    Mathf.Pow(difference, 3f));

            }



            float smoothedValue = zoomSpeed * Time.deltaTime;

            if (smoothedValue > Mathf.Abs(difference))
                smoothedValue = Mathf.Abs(difference);


            // Correct the sign
            smoothedValue *= Mathf.Sign(difference);

            DEB_currentSize = camera.orthographicSize;
            DEB_targetSize = size + difference;

            camera.orthographicSize = size + smoothedValue;
        }

        private float GetTargetZoom(Rect cameraRect, Rect objectBounds, BoundsRestriction boundsRestriction)
        {
            // Zooming 
            float target = GetTargetSize(cameraRect.size, objectBounds.size, boundsRestriction, Vector2.one * 2);

            target = Mathf.Clamp(target, _MinimumSize, _MaximumSize);
            return target;
        }

        private void UpdateCameraPosition(Rect cameraRect, Vector2 targetPosition)
        {
            Vector2 positionDifference = targetPosition - cameraRect.center;
            Vector2 positionTranslation = positionDifference * Time.deltaTime * 5;

            // cap at end location
            if (positionTranslation.sqrMagnitude > positionDifference.sqrMagnitude)
                positionTranslation = positionDifference;

            transform.Translate(positionTranslation);
        }

        private float GetTargetSize(Vector2 current, Vector2 target, BoundsRestriction boundsRestriction, Vector2 margin)
        {
            margin *= (target.magnitude / 8);
            margin.Set(margin.x * GetAspect(current), margin.y);

            DEB_marginX = margin.x;
            DEB_marginY = margin.y;

            target += margin;

            float byHeight = target.x * current.y / current.x * 0.5f;
            float byWidth = current.x * target.y / current.x * 0.5f;
            
            if (boundsRestriction)
            {
                Vector2 max = boundsRestriction.Bounds.size;
                float byMaxHeight = max.x * current.y / current.x * 0.5f;
                float byMaxWidth = current.x * max.y / current.x * 0.5f;
                return Mathf.Min(Mathf.Max(byHeight, byWidth) ,
                                 Mathf.Min(byMaxHeight, byMaxWidth));
            }

            return Mathf.Max(byHeight, byWidth);
        }

        private float GetAspect(Vector2 size)
        {
            if (size.y == 0 && size.x == 0)
                return 0f;

            if (size.y > 0 && size.x == 0)
                return float.MaxValue;

            return size.x / size.y;
        }

        private void OnGUI()
        {
            Rect rect = camera.Get2DCameraRect();

            StringBuilder label = new StringBuilder();
            label.Append("X: ").AppendLine(rect.center.y.ToString());
            label.Append("Y: ").AppendLine(rect.center.x.ToString());
            label.Append("Width: ").AppendLine(rect.width.ToString());
            label.Append("Height: ").AppendLine(rect.height.ToString());
            label.AppendLine();
            label.Append("CurrentSize: ").AppendLine(DEB_currentSize.ToString());
            label.Append("TargetSzie: ").AppendLine(DEB_targetSize.ToString());
            label.Append("Applied margin x: ").AppendLine(DEB_marginX.ToString());
            label.Append("Applied margin y: ").AppendLine(DEB_marginY.ToString());
            label.AppendLine();
            label.Append("Max bounds diff x: ").AppendLine(DEB_XDiff.ToString());
            label.Append("Max bounds diff Y: ").AppendLine(Deb_YDiff.ToString());




            GUILayout.Label(label.ToString());
        }

        private void OnDrawGizmos()
        {
            if (camera)
            {
                Rect cameraRect = camera.Get2DCameraRect();
                Rect objectBounds = FollowObjectBounds;

                Vector2 positionDifference = objectBounds.center - cameraRect.center;

                Gizmos.DrawLine(cameraRect.center, cameraRect.center + positionDifference);
                DrawGizmoRect(camera.Get2DCameraRect(), Color.green);
                DrawGizmoRect(FollowObjectBounds, Color.red);


            }
        }

        private void DrawGizmoRect(Rect rect, Color color)
        {
            Vector2 topLeft = Rect.NormalizedToPoint(rect, new Vector2(0, 1));
            Vector2 topRight = Rect.NormalizedToPoint(rect, new Vector2(1, 1));
            Vector2 bottomLeft = Rect.NormalizedToPoint(rect, new Vector2(0, 0));
            Vector2 bottomRight = Rect.NormalizedToPoint(rect, new Vector2(1, 0));

            Gizmos.color = color;
            Gizmos.DrawSphere(rect.center, 0.1f);
            Gizmos.DrawLine(topLeft, bottomLeft);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.color = Color.white;
        }
    }
}
