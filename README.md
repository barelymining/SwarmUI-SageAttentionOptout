# SwarmUI SageAttention Optout Extension

A [SwarmUI](https://github.com/mcmonkeyprojects/SwarmUI) extension that lets you override the attention implementation per generation — without restarting SwarmUI or ComfyUI.

## Why This Exists

When running ComfyUI with `--use-sage-attention`, SageAttention is applied globally to every model. Some models don't play well with it — you'd normally have to restart ComfyUI with a different flag just to switch. I found the [comfy-sageattention-optout-customnodes](https://github.com/EnviralDesign/comfy-sageattention-optout-customnodes) node by [EnviralDesign](https://github.com/EnviralDesign) genuinely useful for this, and built this SwarmUI extension so the override is just a dropdown in the generate tab — no restarts needed.

## What It Does

Adds an **Attention Override** group to your SwarmUI generate tab with a single dropdown:

| Mode | Effect |
|------|--------|
| `disabled` | No override — uses ComfyUI's global attention setting |
| `sdpa` | Force PyTorch scaled dot-product attention (opt out of SageAttention) |
| `pytorch` | Alias for `sdpa` |
| `sage` | Force SageAttention for this generation |
| `flash` | Force Flash Attention (requires Flash Attention to be installed) |

Set it to `sdpa` before generating with a model that has issues with SageAttention, then flip it back to `disabled` for everything else.

## Requirements

- [SwarmUI](https://github.com/mcmonkeyprojects/SwarmUI)
- A ComfyUI backend (self-start or remote)
- The [comfy-sageattention-optout-customnodes](https://github.com/EnviralDesign/comfy-sageattention-optout-customnodes) ComfyUI node — installable directly from inside SwarmUI (see below)

## Installation

1. Clone this repository into your SwarmUI extensions folder:

```bash
cd SwarmUI/src/Extensions
git clone https://github.com/YOUR_USERNAME/SwarmUI-SageAttentionOptout SageAttentionOptout
```

2. Run the SwarmUI update script to recompile:

```bash
# Windows
update-windows.bat

# Linux / macOS
./update-linuxmac.sh
```

3. Start SwarmUI. In the generate tab, expand the **Attention Override** group and click **Install SageAttention Optout Node** to install the required ComfyUI node.

4. Restart your ComfyUI backend once after node installation (this is a one-time step).

## Usage

1. Expand the **Attention Override** group in the generate tab
2. Select your desired attention mode from the dropdown
3. Leave it on `disabled` when you don't need an override — it adds no overhead and injects nothing into the workflow

## Credits

The underlying ComfyUI node that makes this work is [comfy-sageattention-optout-customnodes](https://github.com/EnviralDesign/comfy-sageattention-optout-customnodes) by [EnviralDesign](https://github.com/EnviralDesign), released under the MIT license. This SwarmUI extension simply wraps it with a UI and auto-installs it.

## License

MIT
