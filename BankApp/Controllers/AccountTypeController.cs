﻿using BankApp.Models.Responses;
using BankApp.Services.AccountTypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Authorize]
[Route("api/account-types")]
[ApiExplorerSettings(GroupName = "Account types")]
public class AccountTypeController : ControllerBase
{
    private readonly IAccountTypeService _accountTypeService;

    public AccountTypeController(IAccountTypeService accountTypeService)
    {
        _accountTypeService = accountTypeService;
    }

    /// <summary>
    /// Returns all account types. For all authenticated users.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IList<AccountTypeDto>>> GetAllAccountTypesAsync()
    {
        var accountTypes = await _accountTypeService.GetAllAccountTypesAsync();
        var accountTypesDto = accountTypes.Select(a => a.ToDto()).ToList();
        return Ok(accountTypesDto);
    }

    /// <summary>
    /// Returns currencies available for specified account type. For all authenticated users.
    /// </summary>
    [HttpGet("{accountTypeId:int}/currencies")]
    public async Task<ActionResult<IList<CurrencyDto>>> GetCurrenciesOfAccountTypeAsync(int accountTypeId)
    {
        var currencies = await _accountTypeService.GetCurrenciesOfAccountTypeAsync(accountTypeId);
        var currenciesDto = currencies.Select(c => c.ToDto()).ToList();
        return Ok(currenciesDto);
    }
}