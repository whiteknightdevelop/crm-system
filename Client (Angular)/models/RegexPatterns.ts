
export const PASSPORT_REGEX = '^[a-zA-Z0-9<]+$';
export const ALPHABETIC_REGEX = '^[a-zA-Z\\u0590-\\u05fe\\s\'\-]+$';
export const ALPHANUMERIC_REGEX = '^[a-zA-Z0-9\.]+$';
export const ALPHANUMERICWITHHEBREW_REGEX = '^[a-zA-Z\\u0590-\\u05fe0-9\\s\'\\-\.\(\)]+$';
export const NUMERIC_REGEX = '(^-?[0-9]+$)|(^$)';
export const NUMERICEMPTYORLENGTH7_REGEX = '(^([1-9]{1}[0-9]{6})$)';
export const PHONENUMBER_REGEX = '^0(([2-4]{1}-\\d{7})|([8-9]{1}-\\d{7})|([5]{1}\\d-\\d{7})|([7]{1}\\d-\\d{7})|([2-4]{1}\\d{7})|([8-9]{1}\\d{7})|([5]{1}\\d\\d{7})|([7]{1}\\d\\d{7}))$';
export const MOBILENUMBER_REGEX = '^0(([5]{1}\\d-\\d{7})|([5]{1}\\d\\d{7}))$';
export const EMAIL_REGEX = '^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$|^no$';
export const WEIGHT_REGEX = '(^0$)|(^100$)|(^[1-9][0-9]$)|(^[1-9]$)|(^(0|100|([1-9][0-9])|([1-9]))(\\.)([0-9]{1,3})$)';
export const TEMPERATURE_REGEX = '(^3[2-9]$)|(^4[0-5]$)|(^0$)|(^((3[2-9])|(4[0-5]))\\.[0-9]$)';
export const PULSE_REGEX = '(^[2-9][0-9]$)|(^[1-2][0-9][0-9]$)|(^300$)|(^0$)';
export const DATE_REGEX = '^(19[0-9]{2}|20[0-4][0-9])-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])$';
export const PRESCRIPTIONDOSAGE_REGEX = '^[a-zA-Z\\u0590-\\u05fe0-9\\s\'\\-\.\(\)\/]+$';
export const SEARCH_PHONENUMBER_REGEX = '^0(([2-4]{1}-\\d{7})|([8-9]{1}-\\d{7})|([5]{1}\\d-\\d{7})|([7]{1}\\d-\\d{7})|([2-4]{1}\\d{7})|([8-9]{1}\\d{7})|([5]{1}\\d\\d{7})|([7]{1}\\d\\d{7}))$';
