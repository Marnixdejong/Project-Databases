import json

file_path = r"c:\Users\marni\Downloads\0kUGwgRW - project-1-databases (1).json"
with open(file_path, 'r', encoding='utf-8') as f:
    data = json.load(f)

lists = {l['id']: l['name'] for l in data.get('lists', [])}
cards = data.get('cards', [])
checklists = {c['id']: c for c in data.get('checklists', [])}

print(f"# Trello Board: {data.get('name', 'Unknown')}")
list_names = ["Backlog", "to do", "doing", "Review", "done"]

for list_name in list_names:
    list_id = next((k for k, v in lists.items() if v.lower() == list_name.lower()), None)
    if not list_id:
        continue
    
    print(f"\n## List: {list_name}")
    for card in cards:
        if card.get('idList') == list_id and not card.get('closed'):
            print(f"\n### {card['name']}")
            if card.get('desc'):
                print(f"**Description:**\n{card['desc']}\n")
            
            for cl_id in card.get('idChecklists', []):
                cl = checklists.get(cl_id)
                if cl:
                    print(f"**Checklist: {cl['name']}**")
                    for item in cl.get('checkItems', []):
                        state = "[x]" if item['state'] == 'complete' else "[ ]"
                        print(f"  {state} {item['name']}")
