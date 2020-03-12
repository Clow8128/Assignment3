//Cameron Low
//Distributed Applications
//Assignment 2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Dtos;
using RESTAPI.Entities;
using RESTAPI.Repositories;

namespace RESTAPI.Controllers
{
    //Controller for songs
    [Route("api/[controller]")]
    public class SongsController : Controller
    {
        private readonly ISongRepository _songRepository;

        public SongsController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        //return list of all songs in json format
        [HttpGet]
        public IActionResult GetAllSongs(bool? isPopular)
        {
            var songs = _songRepository.GetAll().ToList();

            if (isPopular.HasValue)
            {
                songs = songs.FindAll(x => x.IsPopular == isPopular);
            }

            var allSongsDto = songs.Select(x => Mapper.Map<SongDto>(x));

            return Ok(allSongsDto);
        }

        //returns song info for one song based on given id

        [HttpGet]
        [Route("{id}", Name = "GetSingleSong")]
        public IActionResult GetSingleSong(Guid ID)
        {
            var songRepo = _songRepository.GetSingle(ID);

            if (songRepo == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SongDto>(songRepo));
        }

        //api/songs
        //Adds a song to database with given json data
        [HttpPost]
        public IActionResult AddSong([FromBody] SongCreateDto songCreateDto)
        {
            Song toAdd = Mapper.Map<Song>(songCreateDto);

            _songRepository.Add(toAdd);

            bool result = _songRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return CreatedAtRoute("GetSingleSong", new { id = toAdd.ID }, Mapper.Map<SongDto>(toAdd));
        }

        //api/songs/{id}

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateSong(Guid ID, [FromBody] SongUpdateDto updateDto)
        {
            var repoSong = _songRepository.GetSingle(ID);

            if (repoSong == null)
            {
                return NotFound();
            }

            Mapper.Map(updateDto, repoSong);

            _songRepository.Update(repoSong);

            bool result = _songRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(Mapper.Map<SongDto>(repoSong));
        }

        //Update a song with given id
        [HttpPatch]
        [Route("{id}")]
        public IActionResult PartiallyUpdate(Guid ID, [FromBody] JsonPatchDocument<SongUpdateDto> PatchDoc)
        {
            if (PatchDoc == null)
            {
                return BadRequest();
            }

            var reposong = _songRepository.GetSingle(ID);

            if (reposong == null)
            {
                return NotFound();
            }

            var songToPatch = Mapper.Map<SongUpdateDto>(reposong);
            PatchDoc.ApplyTo(songToPatch);

            Mapper.Map(songToPatch, reposong);

            _songRepository.Update(reposong);

            bool result = _songRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(Mapper.Map<SongDto>(reposong));
        }

        //Remove song
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(Guid ID)
        {
            var reposong = _songRepository.GetSingle(ID);

            if (reposong == null)
            {
                return NotFound();
            }

            _songRepository.Delete(ID);

            bool result = _songRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return NoContent();
        }

    }
}