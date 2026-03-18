using System;
using System.IO;

namespace CrudGen
{
    class Program
    {
        static void Main()
        {
            var entities = new[]
            {
                new { 
                    Name = "Activity", 
                    Plural = "Activities", 
                    Props = new[] { ("Name", "text"), ("StartTime", "datetime-local"), ("EndTime", "datetime-local") },
                    DisplayProp = "Name"
                },
                new { 
                    Name = "Room", 
                    Plural = "Rooms", 
                    Props = new[] { ("Building", "text"), ("RoomNumber", "text"), ("SquareMeters", "number"), ("IsTeacherRoom", "checkbox") },
                    DisplayProp = "RoomNumber"
                },
                new { 
                    Name = "Student", 
                    Plural = "Students", 
                    Props = new[] { ("StudentNumber", "text"), ("ClassId", "number"), ("FirstName", "text"), ("LastName", "text"), ("TelephoneNumber", "text") },
                    DisplayProp = "FirstName"
                },
                new { 
                    Name = "Lecturer", 
                    Plural = "Lecturers", 
                    Props = new[] { ("Age", "number"), ("PersonId", "number") },
                    DisplayProp = "Id"
                }
            };

            string outDir = @"C:\Users\marni\Project-Databases\project someren";

            foreach (var e in entities)
            {
                // CONTROLLER
                string cText = $@"
using Microsoft.AspNetCore.Mvc;
using project_someren.Data;
using project_someren.Models;
using System.Linq;

namespace project_someren.Controllers
{{
    public class {e.Plural}Controller : Controller
    {{
        private readonly ApplicationDbContext _context;
        public {e.Plural}Controller(ApplicationDbContext context) {{ _context = context; }}

        public IActionResult Index() => View(_context.{e.Plural}.ToList());

        public IActionResult Create() => View();
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Create({e.Name} obj) {{
            if (ModelState.IsValid) {{ _context.{e.Plural}.Add(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }}
            return View(obj);
        }}

        public IActionResult Edit(int id) {{
            var obj = _context.{e.Plural}.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }}
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Edit({e.Name} obj) {{
            if (ModelState.IsValid) {{ _context.{e.Plural}.Update(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }}
            return View(obj);
        }}

        public IActionResult Delete(int id) {{
            var obj = _context.{e.Plural}.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }}
        [HttpPost, ActionName(""Delete"")] [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {{
            var obj = _context.{e.Plural}.Find(id);
            if (obj != null) {{ _context.{e.Plural}.Remove(obj); _context.SaveChanges(); }}
            return RedirectToAction(nameof(Index));
        }}
    }}
}}";
                File.WriteAllText(Path.Combine(outDir, "Controllers", $"{e.Plural}Controller.cs"), cText);

                // VIEWS
                string vDir = Path.Combine(outDir, "Views", e.Plural);
                Directory.CreateDirectory(vDir);

                // INDEX
                string ths = string.Join("\n                ", e.Props.Select(p => $"<th>{p.Item1}</th>"));
                string tds = string.Join("\n                    ", e.Props.Select(p => p.Item2 == "checkbox" ? $"<td>@(item.{p.Item1} ? \"Yes\" : \"No\")</td>" : $"<td>@item.{p.Item1}</td>"));
                
                string indexText = $@"
@model IEnumerable<project_someren.Models.{e.Name}>
@{{ ViewData[""Title""] = ""{e.Plural}""; }}

<div class=""d-flex justify-content-between align-items-center mb-4 mt-3"">
    <h1 class=""display-5 fw-bold text-primary"">Manage {e.Plural}</h1>
    <a asp-action=""Create"" class=""btn btn-lg btn-success shadow-sm rounded-pill px-4"">
        <i class=""bi bi-plus-circle me-2""></i>Create New
    </a>
</div>

<div class=""card shadow-sm border-0 rounded-4 overflow-hidden mb-5"">
    <div class=""card-body p-0"">
        <div class=""table-responsive"">
            <table class=""table table-hover table-striped align-middle mb-0"">
                <thead class=""table-dark"">
                    <tr>
                        {ths}
                        <th class=""text-end"">Actions</th>
                    </tr>
                </thead>
                <tbody>
                @foreach (var item in Model) {{
                    <tr>
                        {tds}
                        <td class=""text-end"">
                            <div class=""btn-group shadow-sm"" role=""group"">
                                <a asp-action=""Edit"" asp-route-id=""@item.Id"" class=""btn btn-sm btn-outline-primary"">Edit</a>
                                <a asp-action=""Delete"" asp-route-id=""@item.Id"" class=""btn btn-sm btn-outline-danger"">Delete</a>
                            </div>
                        </td>
                    </tr>
                }}
                </tbody>
            </table>
        </div>
    </div>
</div>
";
                File.WriteAllText(Path.Combine(vDir, "Index.cshtml"), indexText);

                // CREATE / EDIT FORM FIELDS
                string formFields = "";
                foreach (var p in e.Props) {
                    if (p.Item2 == "checkbox") {
                        formFields += $@"
            <div class=""form-check form-switch mb-3"">
                <input class=""form-check-input"" asp-for=""{p.Item1}"" />
                <label class=""form-check-label"" asp-for=""{p.Item1}""></label>
            </div>";
                    } else {
                        formFields += $@"
            <div class=""form-floating mb-3 shadow-sm rounded"">
                <input asp-for=""{p.Item1}"" type=""{p.Item2}"" class=""form-control"" placeholder=""{p.Item1}"" required />
                <label asp-for=""{p.Item1}""></label>
                <span asp-validation-for=""{p.Item1}"" class=""text-danger""></span>
            </div>";
                    }
                }

                string createText = $@"
@model project_someren.Models.{e.Name}
@{{ ViewData[""Title""] = ""Create {e.Name}""; }}

<div class=""row justify-content-center mt-5"">
    <div class=""col-md-6"">
        <div class=""card shadow-lg border-0 rounded-4"">
            <div class=""card-header bg-success text-white rounded-top-4 py-3"">
                <h3 class=""mb-0 fw-bold""><i class=""bi bi-plus-lg me-2""></i>Create New {e.Name}</h3>
            </div>
            <div class=""card-body p-5 bg-light"">
                <form asp-action=""Create"">
                    {formFields}
                    <div class=""d-flex justify-content-between mt-4"">
                        <a asp-action=""Index"" class=""btn btn-outline-secondary rounded-pill px-4"">Back to List</a>
                        <button type=""submit"" class=""btn btn-success rounded-pill px-5 shadow"">Create</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
";
                File.WriteAllText(Path.Combine(vDir, "Create.cshtml"), createText);

                string editText = $@"
@model project_someren.Models.{e.Name}
@{{ ViewData[""Title""] = ""Edit {e.Name}""; }}

<div class=""row justify-content-center mt-5"">
    <div class=""col-md-6"">
        <div class=""card shadow-lg border-0 rounded-4"">
            <div class=""card-header bg-primary text-white rounded-top-4 py-3"">
                <h3 class=""mb-0 fw-bold""><i class=""bi bi-pencil-square me-2""></i>Edit {e.Name}</h3>
            </div>
            <div class=""card-body p-5 bg-light"">
                <form asp-action=""Edit"">
                    <input type=""hidden"" asp-for=""Id"" />
                    {formFields}
                    <div class=""d-flex justify-content-between mt-4"">
                        <a asp-action=""Index"" class=""btn btn-outline-secondary rounded-pill px-4"">Back to List</a>
                        <button type=""submit"" class=""btn btn-primary rounded-pill px-5 shadow"">Save Changes</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
";
                File.WriteAllText(Path.Combine(vDir, "Edit.cshtml"), editText);

                string deleteText = $@"
@model project_someren.Models.{e.Name}
@{{ ViewData[""Title""] = ""Delete {e.Name}""; }}

<div class=""row justify-content-center mt-5"">
    <div class=""col-md-6"">
        <div class=""card shadow-lg border-0 rounded-4 border-danger"">
            <div class=""card-header bg-danger text-white rounded-top-4 py-3"">
                <h3 class=""mb-0 fw-bold""><i class=""bi bi-exclamation-triangle-fill me-2""></i>Delete {e.Name}?</h3>
            </div>
            <div class=""card-body p-5 text-center bg-light"">
                <h5 class=""text-muted mb-4"">Are you sure you want to permanently delete this {e.Name.ToLower()}?</h5>
                <h2 class=""text-danger fw-bold mb-5"">@Model.{e.DisplayProp}</h2>
                <form asp-action=""Delete"">
                    <input type=""hidden"" asp-for=""Id"" />
                    <div class=""d-flex justify-content-center gap-3"">
                        <a asp-action=""Index"" class=""btn btn-outline-secondary rounded-pill px-4"">Cancel</a>
                        <button type=""submit"" class=""btn btn-danger rounded-pill px-5 shadow"">Yes, Delete It</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>
";
                File.WriteAllText(Path.Combine(vDir, "Delete.cshtml"), deleteText);
            }
        }
    }
}
