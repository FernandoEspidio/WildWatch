using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace midas.Pages
{
    public class FormModel : PageModel
    {
        [BindProperty]
        [Required]
        public DateTime BirthDate { get; set; }

        [BindProperty]
        [Required]
        public string Gender { get; set; }

        [BindProperty]
        [Required]
        public string City { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Message = "La entrada es inv�lida. Por favor, int�ntalo de nuevo.";
                return Page();
            }

            var userIdString = HttpContext.Session.GetString("UserID");
            if (userIdString == null || !int.TryParse(userIdString, out int userId))
            {
                Message = "No se pudo recuperar el ID de usuario. Por favor, inicie sesi�n nuevamente.";
                return RedirectToPage("/Register");
            }

            string connectionString = "server=localhost;user=root;database=wildwatch;port=3306;password=";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var hoy = DateTime.Today;
                    var edad = hoy.Year - BirthDate.Year;
                    if (BirthDate > hoy.AddYears(-edad)) edad--;

                    // Normaliza el g�nero a los valores esperados por la base de datos
                    string generoNormalizado = Gender switch
                    {
                        "male" => "Masculino",
                        "female" => "Femenino",
                        "other" => "Otro",
                        _ => "Otro", // Considera un valor predeterminado o maneja como un caso de error
                    };

                    var query = @"
                UPDATE Usuario
                SET Edad = @Edad, Genero = @Gender, Localidad = @City
                WHERE ID_Usuario = @UserID;";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@Edad", edad);
                        command.Parameters.AddWithValue("@Gender", generoNormalizado);
                        command.Parameters.AddWithValue("@City", City);

                        await command.ExecuteNonQueryAsync();
                    }
                }
                Message = "Informaci�n guardada exitosamente.";
                return RedirectToPage("/Success");
            }
            catch (Exception ex)
            {
                Message = $"Error al guardar la informaci�n: {ex.Message}";
                return Page();
            }
        }
    }
}
