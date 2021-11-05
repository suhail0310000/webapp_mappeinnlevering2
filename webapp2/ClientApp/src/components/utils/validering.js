export const validereReise = (reise, temp_errors) => {
    let errors = { ...temp_errors };
    console.log(errors);
    console.log(reise);
    let isValid = true;

    for (const [key, value] of Object.entries(reise)) {
        if (typeof value === "string" && value.trim() == "" || value === undefined) {
            errors[key].isValid = false;
            errors[key].message = "kan ikke være tom";
            isValid = false;
        } else if (!isNaN(value) &&  value < 1) {
            errors[key].isValid = false;
            errors[key].message = "kan ikke være mindre eller lik 0";
            isValid = false;
        }
        else {
            errors[key].isValid = true;
            errors[key].message = ""
        }
    }

    let output = {
        isValid: isValid,
        errors: errors
    }

    return output;
}