async function fetchCustomers() {
  try {
    const response = await fetch("https://localhost:7115/api/customers");
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const customers = await response.json();
    const tableBody = document.querySelector("#customer-table tbody");
    tableBody.innerHTML = ""; // Clear existing rows

    customers.forEach((customer) => {
      const row = document.createElement("tr");
      const address = customer.address
        ? `${customer.address.street}, ${customer.address.city}, ${customer.address.country}`
        : "Not specified";

      row.innerHTML = `
            <td>${customer.firstName}</td>
            <td>${customer.lastName}</td>
            <td>${customer.email}</td>
            <td>${customer.phoneNumber}</td>
            <td>${address}</td>
            `;
      tableBody.appendChild(row);
    });
  } catch (error) {
    console.error("Error fetching customers:", error);
  }
}

document.getElementById("add-customer").addEventListener("click", () => {
  document.getElementById("customer-modal").style.display = "flex";
});

document.getElementById("cancel").addEventListener("click", () => {
  document.getElementById("customer-form").reset(); // reset the values of all form fields
  document.getElementById("customer-modal").style.display = "none";
});

document
  .getElementById("phone-number")
  .addEventListener("input", function (event) {
    let phoneNumber = event.target.value;

    // If digit is first entered sign, add a plus
    if (
      phoneNumber &&
      !phoneNumber.startsWith("+") &&
      /^\d/.test(phoneNumber)
    ) {
      phoneNumber = "+" + phoneNumber;
      event.target.value = phoneNumber; // Update field value
    }

    const phoneRegex = /^\+[1-9]\d{1,14}$/;

    // If the entered value does not match the regular expression, remove the last character
    if (!phoneRegex.test(phoneNumber)) {
      event.target.value = phoneNumber.replace(/[^0-9+]/g, "");
    }
  });

document
  .getElementById("customer-form")
  .addEventListener("submit", async (e) => {
    e.preventDefault();

    const customerData = {
      firstName: document.getElementById("first-name").value,
      lastName: document.getElementById("last-name").value,
      email: document.getElementById("email").value,
      phoneNumber: document.getElementById("phone-number").value,
    };

    const phoneRegex = /^\+[1-9]\d{1,14}$/;

    if (!phoneRegex.test(customerData.phoneNumber)) {
      alert("Please enter a valid phone number. Example: +1234567890");
      return;
    }

    const street = document.getElementById("street").value;
    const city = document.getElementById("city").value;
    const country = document.getElementById("country").value;

    if (street && city && country) {
      customerData.address = { street, city, country };
    } else {
      if (street !== "" || city !== "" || country !== "") {
        alert("Please enter the complete address (street, city, and country).");
        return;
      }
    }

    try {
      const response = await fetch("https://localhost:7115/api/customers", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(customerData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        if (response.status === 400) {
          if (errorData.errors) {
            let errorMessages = "";
            for (let field in errorData.errors) {
              errorMessages += `${field}: ${errorData.errors[field].join(
                ", "
              )}\n`;
            }
            alert(`Validation errors: \n${errorMessages}`);
          } else if (errorData.detail) {
            alert(`Error: ${errorData.detail}`);
          } else {
            alert("Bad Request: Invalid input data.");
          }
        } else if (response.status === 500) {
          alert("Something went wrong on the server. Please try again later.");
        } else {
          alert(`Error: ${response.statusText}`);
        }

        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      document.getElementById("customer-modal").style.display = "none";
      location.reload();
    } catch (error) {
      console.error("Error adding customer:", error);
    }
  });

// Fetch customers on page load
document.addEventListener("DOMContentLoaded", fetchCustomers);
