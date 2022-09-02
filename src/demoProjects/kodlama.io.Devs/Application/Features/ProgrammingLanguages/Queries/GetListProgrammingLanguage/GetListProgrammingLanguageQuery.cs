﻿using Application.Features.ProgrammingLanguages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;

public class GetListProgrammingLanguageQuery : IRequest<ProgrammingLanguageListModel>
{
    public PageRequest PageRequest { get; set; }

    public class
        GetListProgrammingLanguageQueryHandler : IRequestHandler<GetListProgrammingLanguageQuery,
            ProgrammingLanguageListModel>
    {
        private readonly IProgrammingLanguageRepository _repository;
        private readonly IMapper _mapper;

        public GetListProgrammingLanguageQueryHandler(IProgrammingLanguageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProgrammingLanguageListModel> Handle(GetListProgrammingLanguageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProgrammingLanguage> programmingLanguages = await _repository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            ProgrammingLanguageListModel mappedLanguageListModel =
                _mapper.Map<ProgrammingLanguageListModel>(programmingLanguages);
            return mappedLanguageListModel;
        }
    }
}