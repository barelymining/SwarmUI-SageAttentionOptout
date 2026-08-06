using Newtonsoft.Json.Linq;
using SwarmUI.Builtin_ComfyUIBackend;
using SwarmUI.Core;
using SwarmUI.Text2Image;
using SwarmUI.Utils;

namespace SwarmUI.Extension_SageAttentionOptout;

public class SageAttentionOptout : Extension
{
    public static T2IRegisteredParam<string> AttentionMode;

    public static T2IParamGroup SageOptoutGroup;

    public override void OnInit()
    {
        InstallableFeatures.RegisterInstallableFeature(new(
            "SageAttention Optout",
            "sage_attention_optout",
            "https://github.com/EnviralDesign/comfy-sageattention-optout-customnodes",
            "EnviralDesign",
            "This will install the Per-Model Attention Override node by EnviralDesign.\nUseful when running ComfyUI with --use-sage-attention but certain models need a different attention mode.\nDo you wish to install?"
        ));

        ScriptFiles.Add("assets/sage_attention_optout.js");

        ComfyUIBackendExtension.NodeToFeatureMap["PerModelAttentionOverride"] = "sage_attention_optout";

        SageOptoutGroup = new T2IParamGroup("Attention Override", Toggles: true, Open: false, IsAdvanced: false);

        AttentionMode = T2IParamTypes.Register<string>(new(
            "Attention Mode",
            "Per-model attention implementation override.\n'disabled' = use ComfyUI global setting (no override).\n'sdpa' or 'pytorch' = force standard PyTorch attention (opt out of SageAttention).\n'sage' = force SageAttention.\n'flash' = force Flash Attention (if available).\nRequires installing the node via the button below.",
            "disabled",
            IgnoreIf: "disabled",
            Group: SageOptoutGroup,
            OrderPriority: 1,
            GetValues: (_) => ["disabled", "sdpa", "pytorch", "sage", "flash"]
        ));

        WorkflowGenerator.AddStep(g =>
        {
            if (g.UserInput.TryGet(AttentionMode, out string mode) && mode != "disabled")
            {
                if (!g.Features.Contains("sage_attention_optout"))
                {
                    throw new SwarmUserErrorException("Attention Override parameter is set but the SageAttention Optout node is not installed. Please install it via the parameter group's install button.");
                }

                string newNode = g.CreateNode("PerModelAttentionOverride", new JObject()
                {
                    ["model"] = g.CurrentModel.Path,
                    ["mode"] = mode
                });

                g.CurrentModel = g.CurrentModel.WithPath([newNode, 0]);
            }
        }, -5.5);
    }
}
