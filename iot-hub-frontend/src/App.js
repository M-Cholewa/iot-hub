import React from "react";
import { ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { darkTheme } from "./core/config/theme.js";
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { DashboardPage } from "./pages/dashboard/dashboard.page.js";
import { DeviceListPage } from "./pages/device/deviceList.page.js";
import { DeviceDetailsPage } from "./pages/device/deviceDetails.page.js";
import { LoginPage } from "./pages/login/login.page.js";
import { RegisterPage } from "./pages/register/register.page.js";
import { AdminPage } from "./pages/admin/admin.page.js";
import { NotFoundPage } from "./pages/error/NotFound.page.js";

export const App = () => {
  return (
    <ThemeProvider theme={darkTheme}>
      <CssBaseline />
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<DashboardPage />} />
          <Route path="/devices" element={<DeviceListPage />} />
          <Route path="/device:id" element={<DeviceDetailsPage />} />
          <Route path="/auth" element={<LoginPage />} />
          <Route path="/auth/login" element={<LoginPage />} />
          <Route path="/auth/register" element={<RegisterPage />} />
          <Route path="/admin" element={<AdminPage />} />

          <Route path="*" element={<NotFoundPage />} />
        </Routes>
      </BrowserRouter>
    </ThemeProvider>
  );
};
