# Hebrew → Gregorian Conversion Test Plan

1. **Single Year Coverage**  
   - Verify `GetHolidaysForGregorianYear` returns all expected 2023 dates (existing NUnit test).

2. **Leap Year Handling**  
   - Input a Hebrew leap year and confirm Adar I/II calculations align with authoritative calendars.

3. **Boundary Spillover**  
   - Ensure holidays that begin in one Gregorian year and finish in the next are correctly filtered.

4. **Fast Observance Adjustments**  
   - Validate postponements such as Tisha B'Av and Tzom Gedaliah when they fall on Shabbat.

5. **Multiple Years Regression**  
   - Iterate across a decade to confirm no missing/duplicate results and that sorting is stable.

6. **Performance Guardrail**  
   - Assert runtime stays within an acceptable threshold when iterating several consecutive years.

7. **Serialization Safety**  
   - Confirm dates round-trip (e.g., to ISO 8601 strings) without losing precision or time zone data.

Update this list as new scenarios emerge to keep parity between documented coverage and automated tests.

