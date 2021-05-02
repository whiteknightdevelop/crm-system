CREATE DEFINER=`root`@`localhost` PROCEDURE `get_debts_sheet_list`()
BEGIN
    SELECT owner_id_num, first_name, last_name, phone, mobile, debt_amount_sum, paid_sum, total_amount, debt_date
    FROM
        (SELECT owner_id_num, debt_amount_sum, paid_sum, (debt_amount_sum - paid_sum ) AS total_amount, debt_date
        FROM
            (SELECT owner_id_num, debt_amount_sum, paid_sum, (debt_amount_sum - paid_sum ) AS total_amount, debt_date
            FROM
                (SELECT owner_id_num, SUM(debt_amount) AS debt_amount_sum, SUM(paid) AS paid_sum, max(debt_date) as debt_date
                FROM petadmin.debts
                GROUP BY owner_id_num) AS sums
            ) AS debts_total_sum
        WHERE total_amount > 0
        ) AS left_debts
    LEFT JOIN
        (SELECT * FROM petadmin.owners) right_owners
    ON left_debts.owner_id_num = right_owners.owner_id
    ORDER BY last_name ASC;
END