﻿using apbd_cw6.Properties.Models;
using apbd_cw6.Properties.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace apbd_cw6.Properties.Controllers;

[ApiController]

// [Route("api/animals")]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult GetAnimals()
    {
        //otwieramy połączneie do bazy
       using SqlConnection connection=new SqlConnection(_configuration.GetConnectionString("Default"));
       connection.Open();
        
       // Definicja command
       SqlCommand command = new SqlCommand();
       command.Connection = connection;
       command.CommandText = "SELECT * FROM  Animal";
       
       //wykonanie zapytania
       var reader = command.ExecuteReader();

       List<Animal> animals = new List<Animal>();
       int idanimalOrdinal = reader.GetOrdinal("IdAnimal");
       int nameOrdinal = reader.GetOrdinal("Name");
       while (reader.Read())
       {
           animals.Add(new Animal()
           {
               IdAnimal = reader.GetInt32(idanimalOrdinal),
               Name = reader.GetString(nameOrdinal)
           });
           
       }

       // var animals = _repository.GetAnimals();
       
       return Ok(animals);
    }

    [HttpPost]
    public IActionResult AddAnimal(AddAnimal addAnimal)
    {
        //otwieramy połączneie do bazy
        using SqlConnection connection=new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        
        // Definicja command
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = $"INSERT INTO  Animal VALUES (@animalName,'','','')";
        //zabezpiecznie przed usunieciem bazy danych przez uzytkowanika @animalName
        command.Parameters.AddWithValue("@animalName", addAnimal.Name);
       
        //wykonanie zapytania
        command.ExecuteNonQuery();
        
        //_repository.AddAnimal(addAnimal);
        return Created();
    }

    [HttpPut("{idAnimal}")]
    public IActionResult PutAnimal(int idAnimal, PutAnimal putAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = @"
        UPDATE Animal
        SET 
            Name = @name,
            Decsription = @decsription,
            Category = @category,
            Area = @area
        WHERE IdAnimal = @idAnimal";

        command.Parameters.AddWithValue("@name", putAnimal.Name);
        command.Parameters.AddWithValue("@decsription", putAnimal.Decsription );
        command.Parameters.AddWithValue("@category", putAnimal.Category );
        command.Parameters.AddWithValue("@area", putAnimal.Area);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);

        try
        {
            command.ExecuteNonQuery();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{idAnimal}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("@idAnimal", idAnimal);

        command.ExecuteNonQuery();

        return Ok(); 
    }


}