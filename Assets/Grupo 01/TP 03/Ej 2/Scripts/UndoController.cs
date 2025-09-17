using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Input demo")]
    public float moveSpeed = 5f;
    public float rotateSpeed = 120f;
    public float scaleSpeed = 1.5f;

    [Header("Undo")]
    public KeyCode undoKey = KeyCode.Z; //tecla de Undo
    public float minMoveDelta = 0.05f;
    public float minRotDelta = 1f;
    public float minScaleDelta = 0.05f;

    private MyStack<IUndoable> history = new MyStack<IUndoable>();    


    private bool isMoving = false;
    private bool isRotating = false;
    private bool isScaling = false;

    private TransformSnapshot? actionStartSnapshot = null;

    private void Awake()
    {
        if (target == null)
        {
            target = transform;
        }

        UpdateUI();
    }

    private void Update()
    {
        bool movedThisFrame = false;
        bool rotatedThisFrame = false;
        bool scaledThisFrame = false;


        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        if (move.sqrMagnitude > 0f)
        {
            if (!isMoving)
            {
                actionStartSnapshot = TransformSnapshot.Capture(target);
                isMoving = true;
            }
            target.localPosition += move.normalized * moveSpeed * Time.deltaTime;
            movedThisFrame = true;
        }
        else if (isMoving)
        {
            FinalizeActionIfSignificant("Move");
            isMoving = false;
        }


        float rot = 0f;
        if (Input.GetKey(KeyCode.Q)) rot += 1f;
        if (Input.GetKey(KeyCode.E)) rot -= 1f;
        if (Mathf.Abs(rot) > 0f)
        {
            if (!isRotating)
            {
                actionStartSnapshot = TransformSnapshot.Capture(target);
                isRotating = true;
            }
            target.localRotation = Quaternion.Euler(0, 0, target.localEulerAngles.z + rot * rotateSpeed * Time.deltaTime);
            rotatedThisFrame = true;
        }
        else if (isRotating)
        {
            FinalizeActionIfSignificant("Rotate");
            isRotating = false;
        }


        float scl = 0f;
        if (Input.GetKey(KeyCode.R)) scl += 1f;
        if (Input.GetKey(KeyCode.F)) scl -= 1f;
        if (Mathf.Abs(scl) > 0f)
        {
            if (!isScaling)
            {
                actionStartSnapshot = TransformSnapshot.Capture(target);
                isScaling = true;
            }
            float factor = 1f + scl * (scaleSpeed - 1f) * Time.deltaTime;
            target.localScale = Vector3.Scale(target.localScale, new Vector3(factor, factor, factor));
            scaledThisFrame = true;
        }
        else if (isScaling)
        {
            FinalizeActionIfSignificant("Scale");
            isScaling = false;
        }

        //Undo (solo una vez al presionar, no mantener presionado)
        if (Input.GetKeyDown(undoKey))
        {
            UndoLast();
        }

        if (movedThisFrame || rotatedThisFrame || scaledThisFrame)
        {
            UpdateUI();
        }
    }

    private void FinalizeActionIfSignificant(string description)
    {
        var endSnapshot = TransformSnapshot.Capture(target);
        if (actionStartSnapshot.HasValue)
        {
            var start = actionStartSnapshot.Value;
            if (Vector3.Distance(endSnapshot.LocalPosition, start.LocalPosition) >= minMoveDelta ||
                Quaternion.Angle(endSnapshot.LocalRotation, start.LocalRotation) >= minRotDelta ||
                Vector3.Distance(endSnapshot.LocalScale, start.LocalScale) >= minScaleDelta)
            {
                history.Push(new TransformUndoAction(start, description));                
            }
        }

        actionStartSnapshot = null;
    }

    public void UndoLast()
    {
        if (history.TryPop(out var action))
        {
            action.Undo();
            UpdateUI();
        }

        else
        {
            Debug.Log("No hay más acciones para deshacer.");
        }
    }

    public string PeekDescription()
    {
        return history.TryPeek(out var action) ? action.Description : "(vacío)";
    }

    private void UpdateUI()
    {
        Debug.Log($"Historial: {history.Count} | Próxima: {PeekDescription()}");
    }
}