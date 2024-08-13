using System.ComponentModel.DataAnnotations;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

/// <summary>
/// Represents the model for retrieving a promotion by its ID in version 1.
/// </summary>
/// <param name="PromotionId">The unique identifier of the promotion.</param>
/// <param name="Status">The current status of the promotion.</param>
/// <param name="CreatedDate">The date when the promotion was created.</param>
/// <param name="LastModifiedDate">The date when the promotion was last modified.</param>
/// <param name="EndValidityDate">The date when the promotion ends or becomes invalid.</param>
/// <param name="Images">A list of image URLs associated with the promotion.</param>
public record PromotionByIdV1Model(
    Guid PromotionId,
    PromotionStatus Status, // ToDo: arreglar esto, porque usa una clase de la capa de entidades.
    DateTime CreatedDate,
    DateTime LastModifiedDate,
    DateTime EndValidityDate,
    List<string> Images);