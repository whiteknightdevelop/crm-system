CREATE DEFINER=`root`@`localhost` PROCEDURE `add_storage_file`(
IN in_file_created DATETIME,
IN in_file_name VARCHAR(128),
IN in_file_path VARCHAR(255),
IN in_file_size BIGINT,
IN in_file_exists TINYINT(1),
IN in_file_extension VARCHAR(45),
IN in_file_hash LONGTEXT,
IN in_file_visitId INT)
BEGIN
    SET @file_exists = 1;
    INSERT INTO petadmin.file_storage (file_created, file_name, file_path, file_size, file_exists, file_extension, file_hash)
    values (
        in_file_created,
        in_file_name,
        in_file_path,
        in_file_size,
        @file_exists,
        in_file_extension,
        in_file_hash);
    SET @last_id = (SELECT LAST_INSERT_ID());
    INSERT INTO petadmin.visit_files (file_id, visit_id)
    values (
    @last_id,
    in_file_visitId);
    SELECT @last_id AS LAST_INSERT_ID;
END