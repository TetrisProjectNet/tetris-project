import time
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options as ChromeOptions
from selenium.webdriver.chrome.service import Service as ChromeService
from selenium import webdriver

service = ChromeService(executable_path="D:/Programozas/Selenium Webdriver/chromedriver", port=6666)
options = ChromeOptions()
options.add_argument("--start-maximized")
driver = webdriver.Chrome(service=service, options=options)

driver.get('http://localhost:4200/home')

driver.switch_to.window(driver.current_window_handle)
time.sleep(1)

shopNavLink = driver.find_element(By.ID, "selenium__shop")
shopNavLink.click()
time.sleep(3)

statisticsNavLink = driver.find_element(By.ID, "selenium__statistics")
statisticsNavLink.click()
time.sleep(3)

signUpNavLink = driver.find_element(By.ID, "selenium__sign__up")
signUpNavLink.click()
time.sleep(3)

loginNavLink = driver.find_element(By.ID, "selenium__login")
loginNavLink.click()
time.sleep(1)

loginUsername = driver.find_element(By.ID, "selenium__login__username")
loginUsername.send_keys("Player")
time.sleep(0.5)
loginPassword = driver.find_element(By.ID, "selenium__login__password")
loginPassword.send_keys("Lolasd123")
time.sleep(0.5)
loginButton = driver.find_element(By.ID, "selenium__login__submit")
time.sleep(0.5)
loginButton.click()
time.sleep(3)

shopNavLink.click()
time.sleep(3)

statisticsNavLink.click()
time.sleep(3)

profileDropdown = driver.find_element(By.ID, "navbarDarkDropdownMenuLink")
profileDropdown.click()
time.sleep(0.5)

logoutButton = driver.find_element(By.ID, "selenium__logout")
logoutButton.click()
time.sleep(3)

# Need this bacause page reloads on logout
shopNavLink = driver.find_element(By.ID, "selenium__shop")
statisticsNavLink = driver.find_element(By.ID, "selenium__statistics")
loginNavLink = driver.find_element(By.ID, "selenium__login")

loginNavLink.click()
time.sleep(0.5)

loginUsername = driver.find_element(By.ID, "selenium__login__username")
loginPassword = driver.find_element(By.ID, "selenium__login__password")
loginButton = driver.find_element(By.ID, "selenium__login__submit")

loginUsername.send_keys("Admin")
time.sleep(0.5)
loginPassword.send_keys("Lolasd123")
loginButton.click()
time.sleep(3)

shopNavLink.click()
time.sleep(3)

statisticsNavLink.click()
time.sleep(3)

usersNavLink = driver.find_element(By.ID, "selenium__users")
usersNavLink.click()
time.sleep(3)

profileDropdown = driver.find_element(By.ID, "navbarDarkDropdownMenuLink")
logoutButton = driver.find_element(By.ID, "selenium__logout")
profileDropdown.click()
time.sleep(0.5)
logoutButton.click()
time.sleep(1)
