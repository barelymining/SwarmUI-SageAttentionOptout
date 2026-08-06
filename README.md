# SwarmUI SageAttention Optout Extension

A [SwarmUI](https://github.com/mcmonkeyprojects/SwarmUI) extension that lets you override the attention implementation per generation without restarting SwarmUI or ComfyUI.

## Why This Exists

When running ComfyUI with `--use-sage-attention`, SageAttention is applied globally to every model. Some models don't play well with it and you'd normally have to restart ComfyUI with a different flag just to switch. I found the [comfy-sageattention-optout-customnodes](https://github.com/EnviralDesign/comfy-sageattention-optout-customnodes) node by [EnviralDesign](https://github.com/EnviralDesign) genuinely useful for this, and built this SwarmUI extension so the override is just a dropdown in the generate tab. No restarts needed.

## What It Does

Adds an **Attention Override** group to your SwarmUI generate tab with a single dropdown.

**disabled** means no override and ComfyUI's global attention setting is used.

**sdpa** forces PyTorch scaled dot product attention, which is the main way to opt out of SageAttention for a specific generation.

**pytorch** is an alias for sdpa.

**sage** forces SageAttention for this generation.

**flash** forces Flash Attention if you have it installed.

Set it to `sdpa` before generating with a model that has issues with SageAttention, then flip it back to `disabled` for everything else.

## Requirements

[SwarmUI](https://github.com/mcmonkeyprojects/SwarmUI) with a ComfyUI backend (self-start or remote).

The [comfy-sageattention-optout-customnodes](https://github.com/EnviralDesign/comfy-sageattention-optout-customnodes) ComfyUI node by EnviralDesign is required and can be installed directly from inside SwarmUI once the extension is loaded.

## Installation

Clone this repository into your SwarmUI extensions folder:

```bash
cd SwarmUI/src/Extensions
git clone https://github.com/barelymining/SwarmUI-SageAttentionOptout SageAttentionOptout
```

Then run the SwarmUI update script to recompile:

```bash
update-windows.bat
```

```bash
./update-linuxmac.sh
```

Start SwarmUI, expand the **Attention Override** group in the generate tab, and click **Install SageAttention Optout Node** to install the required ComfyUI node. Restart your ComfyUI backend once after that and you are good to go.

## Usage

Expand the **Attention Override** group in the generate tab and select your desired attention mode from the dropdown. Leave it on `disabled` when you do not need an override as it adds no overhead and injects nothing into the workflow when set to disabled.

## Credits

The underlying ComfyUI node that makes this work is [comfy-sageattention-optout-customnodes](https://github.com/EnviralDesign/comfy-sageattention-optout-customnodes) by [EnviralDesign](https://github.com/EnviralDesign), released under the MIT license. This SwarmUI extension simply wraps it with a UI and handles auto-installation.

## License

MIT
