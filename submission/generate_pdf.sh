#!/usr/bin/env bash
# generate_pdf.sh – render the Mermaid ERD and convert the submission doc to PDF.
# Run from the repository root: ./submission/generate_pdf.sh
set -e

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"

echo "==> Rendering Mermaid ERD to SVG..."
mmdc -i "$REPO_ROOT/diagrams/erd.mmd" -o "$REPO_ROOT/diagrams/erd.svg"

echo "==> Converting Markdown to PDF..."
# PDF engine: wkhtmltopdf is used by default.
# If you encounter compatibility issues (wkhtmltopdf has limited maintenance since 2020),
# replace --pdf-engine=wkhtmltopdf with --pdf-engine=xelatex or --pdf-engine=lualatex.
pandoc "$REPO_ROOT/submission/Assignment1_Submission.md" \
  --pdf-engine=wkhtmltopdf \
  --metadata title="Project Databases – Assignment 1" \
  -o "$REPO_ROOT/submission/Assignment1_Submission.pdf"

echo "==> Done! PDF saved to submission/Assignment1_Submission.pdf"
