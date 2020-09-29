using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class TargetInfo : MonoBehaviour
{
    void OnGUI()
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        if (GUI.Button(new Rect(50, 50, 200, 40), "Size Up"))
        {
            ObjectTracker tracker = TrackerManager.Instance.GetTracker();
            var activeDataSets = tracker.GetActiveDataSets().ToList();
            foreach (DataSet ds in activeDataSets)
            {
                // Deactivate Dataset before changing the target size
                tracker.DeactivateDataSet(ds);
                foreach (Trackable trackable in ds.GetTrackables())
                {
                    if (trackable is ImageTarget)
                    {
                        ImageTarget it = trackable as ImageTarget; Vector2 old_size = it.GetSize();
                        Vector2 new_size = new Vector2(1.5f * old_size.x, 1.5f * old_size.y);
                        it.SetSize(new_size);
                    }
                }
                // Re-activate dataset
                tracker.ActivateDataSet(ds);
            }
        }

        foreach (TrackableBehaviour tb in sm.GetActiveTrackableBehaviours())
        {
            if (tb is ImageTargetBehaviour)
            {
                ImageTargetBehaviour itb = tb as ImageTargetBehaviour;
                float dist2cam = (itb.transform.position - Camera.main.transform.position).magnitude;
                ImageTarget it = itb.Trackable as ImageTarget; Vector2 size = it.GetSize();
                GUI.Box(new Rect(50, 100, 300, 40), it.Name + " - " + size.ToString() + "\nDistance to camera: " + dist2cam);
            }
        }
    }
}