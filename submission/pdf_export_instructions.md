# How to Export Assignment1_Submission.md to PDF

This guide explains how to turn `submission/Assignment1_Submission.md` (which contains
a Mermaid ERD) into a polished PDF that can be submitted for the rubric.

---

## Option 1 – VS Code + Markdown PDF extension (recommended for Windows / macOS)

1. Install [Visual Studio Code](https://code.visualstudio.com/).
2. Install the **Markdown PDF** extension by yzane
   (`ext install yzane.markdown-pdf`).
3. Open `submission/Assignment1_Submission.md` in VS Code.
4. Press `Ctrl+Shift+P` → type **Markdown PDF: Export (pdf)** → press Enter.
5. The PDF is saved next to the `.md` file.

> **Tip:** Mermaid diagrams are rendered automatically by the extension.

---

## Option 2 – Mermaid CLI + Pandoc (cross-platform, scriptable)

### Prerequisites

```bash
npm install -g @mermaid-js/mermaid-cli   # installs mmdc
# macOS/Linux: brew install pandoc
# Windows:    choco install pandoc
```

### Steps

```bash
# 1. Render the Mermaid diagram to SVG
mmdc -i diagrams/erd.mmd -o diagrams/erd.svg

# 2. Convert the Markdown to PDF via Pandoc
#    (Pandoc uses the SVG by default; ensure the img reference is correct)
pandoc submission/Assignment1_Submission.md \
  --pdf-engine=wkhtmltopdf \
  -o submission/Assignment1_Submission.pdf
```

> **Note:** `wkhtmltopdf` has been in limited maintenance since 2020 and may have
> compatibility issues on newer systems. If you encounter problems, substitute
> `--pdf-engine=xelatex` or `--pdf-engine=lualatex` (both require a TeX distribution
> such as TeX Live or MiKTeX).

---

## Option 3 – GitHub / online Mermaid Live Editor

1. Open <https://mermaid.live>.
2. Paste the contents of `diagrams/erd.mmd` into the editor.
3. Download the diagram as **PNG** or **SVG**.
4. Insert the image into a Word document alongside the rest of the report, then export
   to PDF from Word.

---

## Option 4 – Node.js helper script (`generate_pdf.sh`)

A convenience shell script is provided below. Save it as `submission/generate_pdf.sh`
and run it from the repository root:

```bash
#!/usr/bin/env bash
set -e

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"

echo "==> Rendering Mermaid ERD to SVG..."
mmdc -i "$REPO_ROOT/diagrams/erd.mmd" -o "$REPO_ROOT/diagrams/erd.svg"

echo "==> Converting Markdown to PDF..."
pandoc "$REPO_ROOT/submission/Assignment1_Submission.md" \
  --pdf-engine=wkhtmltopdf \
  --metadata title="Project Databases – Assignment 1" \
  -o "$REPO_ROOT/submission/Assignment1_Submission.pdf"

echo "==> Done! PDF saved to submission/Assignment1_Submission.pdf"
```

Make it executable:

```bash
chmod +x submission/generate_pdf.sh
./submission/generate_pdf.sh
```

---

## Troubleshooting

| Issue | Solution |
|---|---|
| `mmdc: command not found` | Run `npm install -g @mermaid-js/mermaid-cli` first |
| `pandoc: command not found` | Install Pandoc from <https://pandoc.org/installing.html> |
| Mermaid block not rendered in PDF | Use Option 1 (VS Code) or pre-render to SVG first (Option 2) |
| Tables misaligned in PDF | Add `--variable geometry:margin=2cm` to the Pandoc command |
