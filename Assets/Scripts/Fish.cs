using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private BounceWithObject bounceWithObject;
    
    private MaterialPropertyBlock _materialPropertyBlock;
    
    private readonly Color _lightRed = new Color(1f, 0.6f, 0.6f);
    private readonly Color _midRed   = new Color(1f, 0.2f, 0.2f);
    private readonly Color _deepRed  = new Color(0.6f, 0f, 0f);
    
    private int FryCount = 0;
    
    private void Start()
    {
        _materialPropertyBlock = new MaterialPropertyBlock();

        bounceWithObject.OnBounce = OnHitPanFry;
    }
    
    private void OnHitPanFry()
    {
        FryCount++;
        TurnRed();
    }
    
    private void TurnRed()
    {
        _materialPropertyBlock.SetColor("_BaseColor", GetFriedColor()); // or your custom shader property
        objectRenderer.SetPropertyBlock(_materialPropertyBlock);
    }

    private Color GetFriedColor()
    {
        return FryCount switch
        {
            0 => _lightRed,
            1 => _midRed,
            > 1 => _deepRed,
            _ => Color.white
        };
    }
}
