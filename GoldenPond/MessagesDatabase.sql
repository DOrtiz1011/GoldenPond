-- 1. lists of each domain in the system
SELECT DISTINCT UrlText From Domain


-- 2. lists of each email address along with the owner’s real name
SELECT
    EmailAdress.AddressText,
    Person.Name
FROM
    EmailAdress INNER JOIN
    Person ON (EmailAdress.PersonId = Person.Id)


-- 3. lists each domain and the number of emails sent to it (To, CC, or BCC)
SELECT
    Domain.UrlText,
    ParticipantType.Type,
    COUNT(*) AS 'Count'
FROM
    Domain                                                          INNER JOIN
    EmailAdress     ON (Domain.Id = EmailAdress.DomainId)           INNER JOIN
    Participant     ON (EmailAdress.Id = Participant.EmailAdressId) INNER JOIN
    ParticipantType ON (ParticipantType.Id = Participant.ParticipantTypeId)
GROUP BY
    Domain.UrlText,
    ParticipantType.Type
ORDER BY
    Domain.UrlText,
    ParticipantType.Type,
    3 DESC
