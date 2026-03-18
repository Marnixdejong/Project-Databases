import sys
import subprocess
from pathlib import Path

try:
    import pypdf
except ImportError:
    subprocess.check_call([sys.executable, "-m", "pip", "install", "pypdf"])
    import pypdf

def extract_pdf(pdf_path):
    try:
        reader = pypdf.PdfReader(pdf_path)
        text = []
        for page in reader.pages:
            t = page.extract_text()
            if t: text.append(t)
        
        out_path = Path(pdf_path).with_suffix('.txt')
        out_path.write_text('\n'.join(text), encoding='utf-8')
        print(f"Extracted {pdf_path} to {out_path}")
    except Exception as e:
        print(f"Failed {pdf_path}: {e}")

if __name__ == "__main__":
    base_dir = Path(r"c:\Users\marni\Project-Databases")
    for pdf in base_dir.rglob("*.pdf"):
        extract_pdf(str(pdf))
