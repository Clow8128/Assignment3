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
    //Controller to control reviews on songs
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        //Get a list of all reviews in json format
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            var allReviews = _reviewRepository.GetAll().ToList();

            var allReviewsDto = allReviews.Select(x => Mapper.Map<ReviewDto>(x));

            return Ok(allReviewsDto);
        }

        //returns single review based on id
        [HttpGet]
        [Route("{id}", Name = "GetSingleReview")]
        public IActionResult GetSingleReview(Guid ID)
        {
            Review reviewRepo = _reviewRepository.GetSingle(ID);

            if (reviewRepo == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<ReviewDto>(reviewRepo));
        }

        // api/reviews
        //Add a review with given information in json format
        [HttpPost]
        public IActionResult AddReview([FromBody] ReviewCreateDto reviewCreateDto)
        {
            Review toAdd = Mapper.Map<Review>(reviewCreateDto);

            _reviewRepository.Add(toAdd);

            bool result = _reviewRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return CreatedAtRoute("GetSingleReview", new { id = toAdd.ID }, Mapper.Map<ReviewDto>(toAdd));
        }

        // api/reviews/{id}
        //update review given id
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateReview(Guid ID, [FromBody] ReviewUpdateDto updateDto)
        {
            var repoReview = _reviewRepository.GetSingle(ID);

            if (repoReview == null)
            {
                return NotFound();
            }

            Mapper.Map(updateDto, repoReview);

            _reviewRepository.Update(repoReview);

            bool result = _reviewRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(Mapper.Map<ReviewDto>(repoReview));
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult PartiallyUpdate(Guid ID, [FromBody] JsonPatchDocument<ReviewUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var repoReview = _reviewRepository.GetSingle(ID);

            if (repoReview == null)
            {
                return NotFound();
            }

            var reviewToPatch = Mapper.Map<ReviewUpdateDto>(repoReview);
            patchDoc.ApplyTo(reviewToPatch);

            Mapper.Map(reviewToPatch, repoReview);

            _reviewRepository.Update(repoReview);

            bool result = _reviewRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return Ok(Mapper.Map<ReviewDto>(repoReview));
        }


        [HttpDelete]
        [Route("{id}")]
        public IActionResult Remove(Guid ID)
        {
            var repoReview = _reviewRepository.GetSingle(ID);

            if (repoReview == null)
            {
                return NotFound();
            }

            _reviewRepository.Delete(ID);

            bool result = _reviewRepository.Save();

            if (!result)
            {
                return new StatusCodeResult(500);
            }

            return NoContent();
        }

    }
}